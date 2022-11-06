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
    VaultKeep newVaultKeep = GetById(vaultKeepId);
    return newVaultKeep;
  }

  public void Delete(VaultKeep vaultKeep)
  {
    string sql = @"
        DELETE FROM vaultkeeps WHERE id = @Id
        ;";
    _db.Execute(sql, vaultKeep);
  }

  public List<VaultKeep> GetVaultKeepsByVaultId(int vaultId)
  {
    string sql = @"
        SELECT
        vk.*,
        a.*,
        k.creatorId
        FROM vaultkeeps vk
        LEFT JOIN keeps k ON k.id = vk.keepId
        JOIN accounts a ON a.id = k.creatorId
        WHERE vk.vaultId = @vaultId
        GROUP BY v.id
        ;";
    return _db.Query<VaultKeep, Profile, VaultKeep>(sql, (vaultKeep, profile) =>
    {
      vaultKeep.Creator = profile;
      return vaultKeep;
    }).ToList();
  }

  public List<VaultKeep> Get()
  {
    throw new NotImplementedException();
  }

  public VaultKeep GetById(int id)
  {
    string sql = @"
    SELECT
    vk.*
    FROM vaultkeeps vk
    WHERE id = @id
    ;";
    VaultKeep vaultKeep = _db.Query<VaultKeep>(sql, new { id }).FirstOrDefault();
    return vaultKeep;
  }

  public VaultKeep Update(VaultKeep data)
  {
    throw new NotImplementedException();
  }
}
