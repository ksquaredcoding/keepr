namespace keepr.Repositories;

public class VaultKeepsRepository : BaseRepository, IRepository<VaultKeep, int>
{
  public VaultKeepsRepository(IDbConnection db) : base(db)
  {
  }
  public VaultKeep Create(VaultKeep vaultKeep)
  {
    string sql = @"
        INSERT INTO vaultkeeps(vaultId, keepId, creatorId)
        VALUES(@vaultId, @keepId, @creatorId);
        SELECT LAST_INSERT_ID()
        ;";
    int vaultKeepId = _db.ExecuteScalar<int>(sql, vaultKeep);
    vaultKeep.Id = vaultKeepId;
    return vaultKeep;
  }

  public void Delete(VaultKeep vaultKeep)
  {
    string sql = @"
        DELETE FROM vaultkeeps WHERE id = @Id
        ;";
    _db.Execute(sql, vaultKeep);
  }

  public List<VaultKept> GetVaultKeepsByVaultId(int vaultId)
  {
    string sql = @"
        SELECT
        vk.*,
        COUNT(vk.id) AS Kept,
        vk.id AS vaultKeepId,
        vk.vaultId AS vaultId,
        vk.keepId AS id,
        a.*,
        k.*
        FROM vaultkeeps vk
        JOIN keeps k ON k.id = vk.keepId
        JOIN accounts a ON a.id = vk.creatorId
        WHERE vk.vaultId = @vaultId
        GROUP BY vk.id
        ;";
    List<VaultKept> vaultKeeps = _db.Query<VaultKept, Profile, Keep, VaultKept>(sql, (vk, p, k) =>
    {
      vk.Creator = p;
      vk.Name = k.Name;
      vk.Description = k.Description;
      vk.Img = k.Img;
      vk.Views = k.Views;
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
    WHERE vk.id = @vaultKeepId
    ;";
    VaultKeep vaultKeep = _db.Query<VaultKeep>(sql, new { vaultKeepId }).FirstOrDefault();
    return vaultKeep;
  }

  public VaultKeep Update(VaultKeep data)
  {
    throw new NotImplementedException();
  }
}
