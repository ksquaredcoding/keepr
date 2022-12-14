namespace keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultsController : ControllerBase
{
  private readonly VaultsService _vaultsService;
  private readonly Auth0Provider _auth0provider;
  private readonly VaultKeepsService _vaultKeepsService;

  public VaultsController(Auth0Provider auth0provider, VaultsService vaultsService, VaultKeepsService vaultKeepsService)
  {
    _auth0provider = auth0provider;
    _vaultsService = vaultsService;
    _vaultKeepsService = vaultKeepsService;
  }

  [HttpPost]
  [Authorize]
  public async Task<ActionResult<Vault>> CreateVault([FromBody] Vault newVault)
  {
    try
    {
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      newVault.CreatorId = userInfo.Id;
      Vault vault = _vaultsService.CreateVault(newVault);
      vault.Creator = userInfo;
      return Ok(vault);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{vaultId}")]
  public async Task<ActionResult<Vault>> GetVaultById(int vaultId)
  {
    try
    {
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      if (userInfo == null)
      {
        Vault vault2 = _vaultsService.GetVaultByIdNoAuth(vaultId);
        return Ok(vault2);
      }
      Vault vault = _vaultsService.GetVaultById(vaultId, userInfo.Id);
      return Ok(vault);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{vaultId}/keeps")]
  public async Task<ActionResult<List<VaultKeep>>> GetVaultKeepsByVaultId(int vaultId)
  {
    try
    {
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      if (userInfo == null)
      {
        List<VaultKept> vaultKeeps = _vaultKeepsService.GetVaultKeepsNoAuth(vaultId);
        return Ok(vaultKeeps);
      }
      List<VaultKept> vaultKeeps2 = _vaultKeepsService.GetVaultKeeps(vaultId, userInfo.Id);
      return Ok(vaultKeeps2);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpPut("{vaultId}")]
  [Authorize]
  public async Task<ActionResult<Vault>> EditVault(int vaultId, [FromBody] Vault vaultData)
  {
    try
    {
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      vaultData.Id = vaultId;
      Vault vault = _vaultsService.EditVault(vaultData, userInfo.Id);
      return Ok(vault);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpDelete("{vaultId}")]
  [Authorize]
  public async Task<ActionResult<Vault>> RemoveVault(int vaultId)
  {
    try
    {
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      _vaultsService.RemoveVault(vaultId, userInfo.Id);
      return Ok("Vault Deleted");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}