using Microsoft.AspNetCore.Mvc;
using services.beertierlist.api.Controllers.Tiers.Requests;
using services.beertierlist.application.Commands.AddTier;
using services.beertierlist.application.Commands.RemoveTier;
using services.beertierlist.application.Queries.Tiers;
using services.beertierlist.domain.Tierlist;

namespace services.beertierlist.api.Controllers.Tiers;

[ApiController]
[Route("[controller]")]
public class TierController : ControllerBase
{
    [Route("add")]
    [HttpPost]
    public async Task<IActionResult> AddTier([FromBody] AddTierRequest request, [FromServices] IAddTierCommandHandler commandHandler, CancellationToken cancellationToken)
    {
        var command = new AddTierCommand("userId", request.Name);

        await commandHandler.Handle(command, cancellationToken);

        return Ok();
    }

    [Route("remove/{guid:id}")]
    [HttpDelete]
    public async Task<IActionResult> RemoveTier([FromRoute] Guid Id, [FromServices] IRemoveTierCommandHandler commandHandler, CancellationToken cancellationToken)
    {
        var command = new RemoveTierCommand(Id, "userId");

        await commandHandler.Handle(command, cancellationToken);

        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Tier>>> GetTiers([FromServices] ITiersQueryHandler queryHandler, CancellationToken cancellationToken)
    {
        var query = new TiersQuery("userId");

        var tiers = await queryHandler.Handle(query, cancellationToken);

        return Ok(tiers);
    }
}
