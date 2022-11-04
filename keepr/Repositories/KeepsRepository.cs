namespace keepr.Repositories;

public class KeepsRepository : BaseRepository, IRepository<Keep, int>
{
  public KeepsRepository(IDbConnection db) : base(db)
  {
  }

  public Keep Create(Keep newKeep)
  {
    string sql = @"
    INSERT INTO keeps(creatorId, name, description, img, views)
    VALUES(@creatorId, @name, @description, @img, @views);
    SELECT LAST_INSERT_ID()
    ;";
    int keepId = _db.ExecuteScalar<int>(sql, newKeep);
    newKeep.Id = keepId;
    return newKeep;
  }

  public void Delete(int id)
  {
    throw new NotImplementedException();
  }

  public List<Keep> Get()
  {
    string sql = @"
        SELECT
        k.*,
        COUNT(vk.id) AS Kept,
        a.*
        FROM keeps k
        JOIN accounts a ON a.id = k.creatorId
        LEFT JOIN vaultkeeps vk ON vk.keepId = k.id
        GROUP BY k.id
        ;";
    return _db.Query<Keep, Profile, Keep>(sql, (k, p) =>
    {
      k.Creator = p;
      return k;
    }).ToList();
  }

  public Keep GetById(int id)
  {
    throw new NotImplementedException();
  }

  public Keep Update(Keep data)
  {
    throw new NotImplementedException();
  }
}