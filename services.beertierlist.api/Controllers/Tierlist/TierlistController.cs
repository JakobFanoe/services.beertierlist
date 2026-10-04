using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using services.beertierlist.api.Extensions;
using services.beertierlist.application.Commands.AddTierlistEntry;
using services.beertierlist.application.Commands.RemoveTierlistEntry;
using services.beertierlist.application.Queries.TierlistEntries;

namespace services.beertierlist.api.Controllers.Tierlist;

[ApiController]
[Route("[controller]")]
[Authorize]
public class TierlistController : ControllerBase
{
    [HttpPost("AddEntry")]
    public async Task<IActionResult> AddTierlistEntry(IFormFile image, [FromServices] IAddTierlistEntryCommandHandler commandHandler)
    {
        if (image == null || image.Length == 0)
            return BadRequest("No image provided.");

        var allowedTypes = new[]
        {
            "image/jpeg",
            "image/png",
            "image/webp"
        };

        if (!allowedTypes.Contains(image.ContentType))
            return BadRequest("Invalid image type.");

        await using var stream = image.OpenReadStream();

        var userId = HttpContext.GetUserId();
        if (userId is null) return Unauthorized();

        var command = new AddTierlistEntryCommand(stream, image.ContentType, image.FileName, userId);

        await commandHandler.Handle(command, HttpContext.RequestAborted);

        return Ok();
    }

    [HttpDelete("RemoveEntry/{id:guid}")]
    public async Task<IActionResult> RemoveTierlistEntry([FromRoute] Guid id, [FromServices] IRemoveTierlistEntryCommandHandler commandHandler)
    {
        var userId = HttpContext.GetUserId();
        if (userId is null) return Unauthorized();

        var command = new RemoveTierlistEntryCommand(id, userId);
        await commandHandler.Handle(command, HttpContext.RequestAborted);

        return Ok();
    }

    [HttpGet("GetEntries")]
    public async Task<IActionResult> GetTierlistEntries([FromServices] ITierlistEntriesQueryHandler queryHandler)
    {
        var userId = HttpContext.GetUserId();
        if (userId is null) return Unauthorized();

        var query = new TierlistEntriesQuery(userId);
        var entries = await queryHandler.Handle(query, HttpContext.RequestAborted);
        return Ok(entries);
    }
}
