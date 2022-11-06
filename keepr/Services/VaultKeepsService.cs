namespace keepr.Services;

public class VaultKeepsService
{
  private readonly VaultKeepsRepository _vaultKeepsRepository;
  private readonly KeepsService _keepsService;
  private readonly VaultsService _vaultsService;

  public VaultKeepsService(VaultKeepsRepository vaultKeepsRepository, KeepsService keepsService, VaultsService vaultsService)
  {
    _vaultKeepsRepository = vaultKeepsRepository;
    _keepsService = keepsService;
    _vaultsService = vaultsService;
  }

  internal VaultKeep CreateVaultKeep(VaultKeep newVaultKeep, string accountId)
  {
    Vault vault = _vaultsService.GetVaultById(newVaultKeep.VaultId, accountId);
    if (vault.CreatorId != accountId)
    {
      throw new Exception("You can only add Keeps to your own Vault");
    }
    Keep keep = _keepsService.GetKeepById2(newVaultKeep.KeepId);
    newVaultKeep.CreatorId = keep.CreatorId;
    // newVaultKeep.VaultKeepId = keep.Id;
    VaultKeep vaultKeep = _vaultKeepsRepository.Create(newVaultKeep);
    // vaultKeep.Name = keep.Name;
    // vaultKeep.Description = keep.Description;
    // vaultKeep.Img = keep.Img;
    // vaultKeep.Views = keep.Views;
    // vaultKeep.Creator = keep.Creator;
    return vaultKeep;
  }

  internal List<VaultKept> GetVaultKeepsNoAuth(int vaultId)
  {
    Vault vault = _vaultsService.GetVaultByIdNoAuth(vaultId);
    return _vaultKeepsRepository.GetVaultKeepsByVaultId(vault.Id);
  }

  internal List<VaultKept> GetVaultKeeps(int vaultId, string accountId)
  {
    Vault vault = _vaultsService.GetVaultById(vaultId, accountId);
    return _vaultKeepsRepository.GetVaultKeepsByVaultId(vault.Id);
  }

  internal void RemoveVaultKeep(int vaultKeepId, string accountId)
  {
    VaultKeep vaultKeep = GetVaultKeepById(vaultKeepId);
    Vault vault = _vaultsService.GetVaultById(vaultKeep.VaultId, accountId);
    if (vault.CreatorId != accountId)
    {
      throw new Exception("You can only delete your own VaultKeeps");
    }
    _vaultKeepsRepository.Delete(vaultKeep);
  }

  internal VaultKeep GetVaultKeepById(int vaultKeepId)
  {
    VaultKeep vaultKeep = _vaultKeepsRepository.GetById(vaultKeepId);
    if (vaultKeep == null)
    {
      throw new Exception("That VaultKeep does not exist");
    }
    return vaultKeep;
  }
}