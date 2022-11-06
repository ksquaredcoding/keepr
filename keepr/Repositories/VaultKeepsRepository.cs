namespace keepr.Repositories;

public class VaultKeepsRepository : BaseRepository, IRepository<VaultKeep, int>
{
  public VaultKeepsRepository(IDbConnection db) : base(db)
  {
  }
  public VaultKeep Create(VaultKeep vaultKeep)
  {
    string sql = @"
        INSERT INTO vaultkeeps(vaultKeepId, vaultId, keepId, creatorId)
        VALUES(@vaultKeepId, @vaultId, @keepId, @creatorId)
        ;";
    _db.Execute(sql, vaultKeep);
    vaultKeep.Id = vaultKeep.KeepId;
    return vaultKeep;
  }

  public void Delete(VaultKeep vaultKeep)
  {
    string sql = @"
        DELETE FROM vaultkeeps WHERE vaultKeepId = @VaultKeepId
        ;";
    _db.Execute(sql, vaultKeep);
  }

  public List<VaultKeep> GetVaultKeepsByVaultId(int vaultId)
  {
    string sql = @"
        SELECT
        vk.*,
        k.*,
        a.*,
        COUNT(vk.vaultKeepId) AS Kept
        FROM vaultkeeps vk
        JOIN keeps k ON k.id = vk.keepId
        JOIN accounts a ON a.id = vk.creatorId
        WHERE vk.vaultId = @vaultId
        GROUP BY vk.vaultKeepId
        ;";
    List<VaultKeep> vaultKeeps = _db.Query<VaultKeep, Keep, Profile, VaultKeep>(sql, (vk, k, p) =>
    {
      vk.Creator = p;
      vk.Name = k.Name;
      vk.Description = k.Description;
      vk.Img = k.Img;
      vk.Views = k.Views;
      vk.Id = k.Id;
      return vk;
    }, new { vaultId }).ToList();
    return vaultKeeps;
  }

  public List<VaultKeep> Get()
  {
    throw new NotImplementedException();
  }

  public VaultKeep GetById(int vaultKeepId)
  {
    string sql = @"
    SELECT
    vk.*
    FROM vaultkeeps vk
    WHERE vk.vaultKeepId = @vaultKeepId
    ;";
    VaultKeep vaultKeep = _db.Query<VaultKeep>(sql, new { vaultKeepId }).FirstOrDefault();
    return vaultKeep;
  }

  public VaultKeep Update(VaultKeep data)
  {
    throw new NotImplementedException();
  }
}
