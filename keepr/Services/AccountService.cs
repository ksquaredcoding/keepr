namespace keepr.Services;

public class AccountService
{
  private readonly AccountsRepository _repo;

  public AccountService(AccountsRepository repo)
  {
    _repo = repo;
  }

  internal Account GetProfileByEmail(string email)
  {
    return _repo.GetByEmail(email);
  }

  internal Account GetById(string id)
  {
    return _repo.GetById(id);
  }

  internal Account GetOrCreateProfile(Account userInfo)
  {
    Account profile = _repo.GetById(userInfo.Id);
    if (profile == null)
    {
      return _repo.Create(userInfo);
    }
    return profile;
  }

  internal Account Edit(Account editData, string userEmail)
  {
    Account original = GetProfileByEmail(userEmail);
    original.Name = editData.Name ?? original.Name;
    original.Picture = editData.Picture ?? original.Picture;
    original.CoverImg = editData.CoverImg ?? original.CoverImg;
    return _repo.Edit(original);
  }

  internal List<Vault> GetMyVaults(string accountId)
  {
    List<Vault> vaults = _repo.GetMyVaults(accountId);
    // List<Vault> filtered = vaults.ForEach(v =>
    // {
    //   v.Creator = profile;
    //   v.CreatorId = accountId;
    // });
    return vaults;
  }

  internal List<VaultKeep> GetAccountVaultKeeps(string id)
  {
    return _repo.GetAccountVaultKeeps(id);
  }
}
