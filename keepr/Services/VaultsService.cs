namespace keepr.Services;

public class VaultsService
{
  private readonly VaultsRepository _vaultsRepository;

  public VaultsService(VaultsRepository vaultsRepository)
  {
    _vaultsRepository = vaultsRepository;
  }

  internal Vault CreateVault(Vault newVault)
  {
    return _vaultsRepository.Create(newVault);
  }

  internal Vault GetVaultById(int vaultId, string accountId)
  {
    Vault vault = _vaultsRepository.GetById(vaultId);
    if (vault == null)
    {
      throw new Exception("Vault does not exist");
    }
    if (vault.IsPrivate && vault.CreatorId != accountId)
    {
      throw new Exception("This vault is private");
    }
    return vault;
  }

  internal Vault GetVaultByIdNoAuth(int vaultId)
  {
    Vault vault = _vaultsRepository.GetById(vaultId);
    if (vault == null)
    {
      throw new Exception("Vault does not exist");
    }
    if (vault.IsPrivate)
    {
      throw new Exception("This vault is private");
    }
    return vault;
  }

  internal Vault EditVault(Vault vaultData, string accountId)
  {
    Vault orig = GetVaultById(vaultData.Id, accountId);
    if (orig.CreatorId != accountId)
    {
      throw new Exception("This vault is not yours, you cannot edit it");
    }
    orig.Name = vaultData.Name ?? orig.Name;
    orig.Description = vaultData.Description ?? orig.Description;
    orig.Img = vaultData.Img ?? orig.Img;
    orig.IsPrivate = vaultData.IsPrivate == false ? orig.IsPrivate : vaultData.IsPrivate;
    return _vaultsRepository.Update(orig);
  }

  internal void RemoveVault(int vaultId, string accountId)
  {
    Vault vault = GetVaultById(vaultId, accountId);
    if (vault.CreatorId != accountId)
    {
      throw new Exception("This vault is not yours, you cannot delete it");
    }
    _vaultsRepository.Delete(vault);
  }
}