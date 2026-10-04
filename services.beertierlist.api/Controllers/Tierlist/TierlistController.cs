using Microsoft.AspNetCore.Mvc;

namespace services.beertierlist.api.Controllers.Tierlist;

[ApiController]
[Route("[controller]")]
public class TierlistController : ControllerBase
{
    [Route("AddEntry")]
    public async Task<IActionResult> AddTierlistEntry()
    {
        return Ok();
    }
}
