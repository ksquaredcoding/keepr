namespace keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class KeepsController : ControllerBase
{
  private readonly KeepsService _keepsService;
  private readonly Auth0Provider _auth0provider;

  public KeepsController(KeepsService keepsService, Auth0Provider auth0provider)
  {
    _keepsService = keepsService;
    _auth0provider = auth0provider;
  }

  [HttpGet]
  public ActionResult<List<Keep>> GetAllKeeps([FromQuery] string offset, [FromQuery] string take)
  {
    try
    {
      List<Keep> keeps = _keepsService.GetAllKeeps();
      return Ok(keeps);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{keepId}")]
  public ActionResult<Keep> GetKeepById(int keepId)
  {
    try
    {
      Keep keep = _keepsService.GetKeepById(keepId);
      return Ok(keep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpPost]
  [Authorize]
  public async Task<ActionResult<Keep>> Create([FromBody] Keep newKeep)
  {
    try
    {
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      newKeep.CreatorId = userInfo.Id;
      Keep createdKeep = _keepsService.CreateKeep(newKeep);
      createdKeep.Creator = userInfo;
      return Ok(createdKeep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpPut("{keepId}")]
  [Authorize]
  public async Task<ActionResult<Keep>> EditKeep(int keepId, [FromBody] Keep keepData)
  {
    try
    {
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      keepData.Id = keepId;
      Keep keep = _keepsService.EditKeep(keepData, userInfo.Id);
      return Ok(keep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpDelete("{keepId}")]
  [Authorize]
  public async Task<ActionResult<Keep>> RemoveKeep(int keepId)
  {
    try
    {
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      _keepsService.RemoveKeep(keepId, userInfo.Id);
      return Ok("Keep Deleted");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}