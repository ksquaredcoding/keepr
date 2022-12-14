namespace keepr.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
  private readonly AccountService _accountService;
  private readonly Auth0Provider _auth0Provider;

  public AccountController(AccountService accountService, Auth0Provider auth0Provider)
  {
    _accountService = accountService;
    _auth0Provider = auth0Provider;
  }

  [HttpGet]
  [Authorize]
  public async Task<ActionResult<Account>> Get()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_accountService.GetOrCreateProfile(userInfo));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("vaults")]
  public async Task<ActionResult<List<Vault>>> GetMyVaults()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      List<Vault> vaults = _accountService.GetMyVaults(userInfo.Id);
      return Ok(vaults);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpPut]
  [Authorize]
  public async Task<ActionResult<Account>> EditAccount([FromBody] Account data)
  {
    Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
    var edited = _accountService.Edit(data, userInfo.Email);
    return Ok(edited);
  }

  [HttpGet("vaultkeeps")]
  [Authorize]
  public async Task<ActionResult<List<VaultKeep>>> GetAccountVaultKeeps()
  {
    Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
    List<VaultKeep> vaultKeeps = _accountService.GetAccountVaultKeeps(userInfo.Id);
    return Ok(vaultKeeps);
  }
}
