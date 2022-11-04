namespace keepr.Repositories;

public class VaultsRepository : BaseRepository, IRepository<Vault, int>
{
  public VaultsRepository(IDbConnection db) : base(db)
  {
  }

  public Vault Create(Vault vault)
  {
    string sql = @"
        INSERT INTO vaults(creatorId, name, description, img, isPrivate)
        VALUES(@creatorId, @name, @description, @img, @isPrivate);
        SELECT LAST_INSERT_ID()
        ;";
    int vaultId = _db.ExecuteScalar<int>(sql, vault);
    vault.Id = vaultId;
    return vault;
  }

  public void Delete(Vault vault)
  {
    string sql = @"
        DELETE FROM vaults WHERE id = @Id
        ;";
    _db.Execute(sql, vault);
  }

  public List<Vault> Get()
  {
    throw new NotImplementedException();
  }

  public Vault GetById(int id)
  {
    string sql = @"
    SELECT
    v.*,
    a.*
    FROM vaults v
    JOIN accounts a on a.id = v.creatorId
    WHERE v.id = @id
    ;";
    return _db.Query<Vault, Profile, Vault>(sql, (v, p) =>
    {
      v.Creator = p;
      return v;
    }, new { id }).FirstOrDefault();
  }

  public Vault Update(Vault vaultData)
  {
    string sql = @"
        UPDATE vaults SET
          name = @name,
          description = @description,
          img = @img,
          isPrivate = @isPrivate
        WHERE id = @id
        ;";
    vaultData.UpdatedAt = DateTime.Now;
    int affectedRows = _db.Execute(sql, vaultData);
    if (affectedRows == 0)
    {
      throw new Exception("No changes made to Vault.");
    }
    return vaultData;
  }
}
