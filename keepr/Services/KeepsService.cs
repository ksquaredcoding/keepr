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

  internal Keep CreateKeep(Keep newKeep)
  {
    return _keepsRepository.Create(newKeep);
  }
}