namespace keepr.Services;

public class KeepsService
{
  private readonly KeepsRepository _keepsRepository;

  public KeepsService(KeepsRepository keepsRepository)
  {
    _keepsRepository = keepsRepository;
  }

  internal List<Keep> GetAllKeeps()
  {
    return _keepsRepository.Get();
  }

  internal Keep GetKeepById(int keepId)
  {
    Keep keep = _keepsRepository.GetById(keepId);
    if (keep == null)
    {
      throw new Exception("Keep does not exist");
    }
    keep.Views++;
    UpdateKeep(keep);
    return keep;
  }

  internal Keep CreateKeep(Keep newKeep)
  {
    return _keepsRepository.Create(newKeep);
  }

  internal Keep EditKeep(Keep keepData, string accountId)
  {
    Keep orig = GetKeepById(keepData.Id);
    if (orig.CreatorId != accountId)
    {
      throw new Exception("You can only edit your own keeps!");
    }
    orig.Name = keepData.Name ?? orig.Name;
    orig.Description = keepData.Description ?? orig.Description;
    orig.Img = keepData.Img ?? orig.Img;
    return _keepsRepository.Update(orig);
  }

  public void UpdateKeep(Keep k)
  {
    _keepsRepository.UpdateViews(k);
  }

  internal void RemoveKeep(int keepId, string accountId)
  {
    Keep keep = GetKeepById(keepId);
    if (keep.CreatorId != accountId)
    {
      throw new Exception("You can only delete your own keeps!");
    }
    _keepsRepository.Delete(keep);
  }
}