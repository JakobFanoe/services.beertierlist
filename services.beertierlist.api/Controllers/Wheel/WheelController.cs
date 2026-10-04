using Microsoft.AspNetCore.Mvc;
using services.beertierlist.api.Controllers.Wheel.Requests;
using services.beertierlist.application.Commands.AddWheelOptions;
using services.beertierlist.application.Commands.RemoveWheelOption;
using services.beertierlist.application.Queries.WheelOptions;
using services.beertierlist.domain.Wheel;

namespace services.beertierlist.api.Controllers.Wheel;

[ApiController]
[Route("[controller]")]
public class WheelController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<WheelOption>>> GetWheelOptions([FromServices] IWheelOptionsQueryHandler wheelOptionsQueryHandler)
    {
        var options = await wheelOptionsQueryHandler.Handle(new WheelOptionsQuery("userId"), HttpContext.RequestAborted);

        return Ok(options);
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddWheelOption([FromBody] AddWheelOptionsRequest request, [FromServices] IAddWheelOptionsCommandHandler commandHandler)
    {
        var command = new AddWheelOptionsCommand("userId", request.WheelOptions);

        await commandHandler.Handle(command, HttpContext.RequestAborted);

        return Ok();
    }

    [HttpDelete("remove/{id:guid}")]
    public async Task<IActionResult> RemoveWheelOption([FromRoute] Guid Id, [FromServices] IRemoveWheelOptionCommandHandler commandHandler)
    {
        var command = new RemoveWheelOptionCommand(Id, "userId");

        await commandHandler.Handle(command, HttpContext.RequestAborted);

        return Ok();
    }
}
