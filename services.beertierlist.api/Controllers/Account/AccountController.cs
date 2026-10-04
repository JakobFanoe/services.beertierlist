using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using services.beertierlist.api.Controllers.Account.Requests;
using services.beertierlist.application.Commands.LoginAccount;
using services.beertierlist.application.Commands.RegisterAccount;

namespace services.beertierlist.api.Controllers.Account;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(
        [FromBody] RegisterRequest request,
        [FromServices] IRegisterAccountCommandHandler commandHandler,
        CancellationToken cancellationToken)
    {
        var command = new RegisterAccountCommand(request.Username, request.Password);
        var result = await commandHandler.Handle(command, cancellationToken);

        return result.Status switch
        {
            RegisterAccountStatus.Created => StatusCode(StatusCodes.Status201Created, new { Id = result.UserId, UserName = result.Username }),
            RegisterAccountStatus.UsernameUnavailable => Conflict("Unable to register with the supplied username."),
            _ => BadRequest("Unable to register with the supplied credentials.")
        };
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequest request,
        [FromServices] ILoginAccountCommandHandler commandHandler,
        CancellationToken cancellationToken)
    {
        var command = new LoginAccountCommand(request.Username, request.Password);
        var token = await commandHandler.Handle(command, cancellationToken);
        if (token is null)
            return Unauthorized();

        return Ok(new { token });
    }
}
