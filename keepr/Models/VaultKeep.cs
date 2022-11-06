namespace keepr.Models;

public class VaultKeep : Keep
{
  public int VaultId { get; set; }
  public int KeepId { get; set; }
  public int VaultKeepId { get; set; }
}