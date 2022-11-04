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

  public void Delete(Keep keepData)
  {
    string sql = @"
        DELETE FROM keeps WHERE id = @Id
        ;";
    _db.Execute(sql, keepData);
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
    string sql = @"
    SELECT
    k.*,
    COUNT(vk.id) AS Kept,
    a.*
    FROM keeps k
    JOIN accounts a ON a.id = k.creatorId
    LEFT JOIN vaultkeeps vk ON vk.keepId = k.id
    WHERE k.id = @id
    GROUP BY k.id
    ;";
    return _db.Query<Keep, Profile, Keep>(sql, (k, p) =>
    {
      k.Creator = p;
      return k;
    }, new { id }).FirstOrDefault();
  }

  public Keep Update(Keep keepData)
  {
    string sql = @"
        UPDATE keeps SET
          name = @name,
          description = @description,
          img = @img,
          views = @views
        WHERE id = @id
        ;";
    keepData.UpdatedAt = DateTime.Now;
    int affectedRows = _db.Execute(sql, keepData);
    if (affectedRows == 0)
    {
      throw new Exception("No changes made to Keep.");
    }
    return keepData;
  }

  public Keep UpdateViews(Keep keepData)
  {
    string sql = @"
        UPDATE keeps SET
          views = @views
        WHERE id = @id
        ;";
    int affectedRows = _db.Execute(sql, keepData);
    if (affectedRows == 0)
    {
      throw new Exception("No changes made to Keep.");
    }
    return keepData;
  }
}