namespace keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultKeepsController : ControllerBase
{
  private readonly VaultKeepsService _vaultKeepsService;
  private readonly Auth0Provider _auth0provider;
  public VaultKeepsController(VaultKeepsService vaultKeepsService, Auth0Provider auth0provider)
  {
    _vaultKeepsService = vaultKeepsService;
    _auth0provider = auth0provider;
  }

  [HttpPost]
  [Authorize]
  public async Task<ActionResult<VaultKeep>> CreateVaultKeep([FromBody] VaultKeep newVaultKeep)
  {
    try
    {
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      VaultKeep vaultKeep = _vaultKeepsService.CreateVaultKeep(newVaultKeep, userInfo.Id);
      return Ok(vaultKeep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpDelete("{vaultKeepId}")]
  [Authorize]
  public async Task<ActionResult<VaultKeep>> RemoveVaultKeep(int vaultKeepId)
  {
    try
    {
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      _vaultKeepsService.RemoveVaultKeep(vaultKeepId, userInfo.Id);
      return Ok("VaultKeep Deleted");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{vaultKeepId}")]
  public ActionResult<VaultKeep> GetVaultKeepById(int vaultKeepId)
  {
    try
    {
      VaultKeep vaultKeep = _vaultKeepsService.GetVaultKeepById(vaultKeepId);
      return Ok(vaultKeep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}