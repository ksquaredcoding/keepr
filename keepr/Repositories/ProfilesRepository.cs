namespace keepr.Repositories;

public class ProfilesRepository : BaseRepository
{
  public ProfilesRepository(IDbConnection db) : base(db)
  {
  }

  internal Profile GetProfileById(string profileId)
  {
    string sql = @"
    SELECT
    a.*
    FROM accounts a
    WHERE a.id = @profileId
    ;";
    return _db.Query<Profile>(sql, new { profileId }).FirstOrDefault();
  }

  internal List<Keep> GetKeepsByProfileId(Profile profile)
  {
    string sql = @"
        SELECT
        k.*,
        COUNT(vk.id) AS Kept,
        a.*
        FROM keeps k
        JOIN accounts a ON a.id = k.creatorId
        LEFT JOIN vaultkeeps vk ON vk.keepId = k.id
        WHERE k.creatorId = @Id
        GROUP BY k.id
        ;";
    return _db.Query<Keep, Profile, Keep>(sql, (k, p) =>
    {
      k.Creator = p;
      return k;
    }, new { profile.Id }).ToList();
  }

  internal List<Vault> GetVaultsByProfileId(string profileId)
  {
    string sql = @"
    SELECT
    v.*
    FROM vaults v
    WHERE v.creatorId = @profileId
    ;";
    return _db.Query<Vault>(sql, new { profileId }).ToList();
  }
}