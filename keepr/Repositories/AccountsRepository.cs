namespace keepr.Repositories;

public class AccountsRepository
{
  private readonly IDbConnection _db;

  public AccountsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Account GetByEmail(string userEmail)
  {
    string sql = "SELECT * FROM accounts WHERE email = @userEmail";
    return _db.QueryFirstOrDefault<Account>(sql, new { userEmail });
  }

  internal Account GetById(string id)
  {
    string sql = "SELECT * FROM accounts WHERE id = @id";
    return _db.QueryFirstOrDefault<Account>(sql, new { id });
  }

  internal Account Create(Account newAccount)
  {
    string sql = @"
            INSERT INTO accounts
              (name, picture, email, id)
            VALUES
              (@Name, @Picture, @Email, @Id)";
    _db.Execute(sql, newAccount);
    return newAccount;
  }

  internal Account Edit(Account update)
  {
    string sql = @"
            UPDATE accounts
            SET 
              name = @Name,
              picture = @Picture,
              coverImg = @CoverImg
            WHERE id = @Id;";
    _db.Execute(sql, update);
    return update;
  }

  internal List<Vault> GetMyVaults(string accountId)
  {
    string sql = @"
    SELECT
    v.*
    FROM vaults v
    WHERE v.creatorId = @accountId
    ;";
    return _db.Query<Vault>(sql, new { accountId }).ToList();
  }

  internal List<VaultKeep> GetAccountVaultKeeps(string id)
  {
    string sql = @"
        SELECT
        v.id,
        vk.*
        FROM vaults v
        JOIN vaultkeeps vk ON vk.vaultId = v.id
        WHERE v.creatorId = @id
        ;";
    return _db.Query<Vault, VaultKeep, VaultKeep>(sql, (v, vk) =>
    {
      return vk;
    }, new { id }).ToList();
  }
}

