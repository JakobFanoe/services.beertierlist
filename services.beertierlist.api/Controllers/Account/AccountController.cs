using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using services.beertierlist.api.Authentication;
using services.beertierlist.api.Configuration;
using services.beertierlist.api.Controllers.Account.Requests;
using services.beertierlist.application.Commands.LoginAccount;
using services.beertierlist.application.Commands.RegisterAccount;

namespace services.beertierlist.api.Controllers.Account;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private const string RefreshTokenCookieName = "beer-tierlist-refresh";
    private static readonly TimeSpan RefreshTokenLifetime = TimeSpan.FromDays(30);

    [HttpPost("register")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(RegisterResponse), StatusCodes.Status201Created)]
    public async Task<ActionResult<RegisterResponse>> Register(
        [FromBody] RegisterRequest request,
        [FromServices] IRegisterAccountCommandHandler commandHandler,
        CancellationToken cancellationToken)
    {
        var command = new RegisterAccountCommand(request.Username, request.Password);
        var result = await commandHandler.Handle(command, cancellationToken);

        return result.Status switch
        {
            RegisterAccountStatus.Created => StatusCode(StatusCodes.Status201Created, new RegisterResponse(result.UserId!, result.Username!)),
            RegisterAccountStatus.UsernameUnavailable => Conflict("Unable to register with the supplied username."),
            _ => BadRequest("Unable to register with the supplied credentials.")
        };
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<AuthTokenResponse>> Login(
        [FromBody] LoginRequest request,
        [FromServices] ILoginAccountCommandHandler commandHandler,
        [FromServices] UserManager<IdentityUser> userManager,
        [FromServices] IRefreshTokenService refreshTokenService,
        CancellationToken cancellationToken)
    {
        var command = new LoginAccountCommand(request.Username, request.Password);
        var token = await commandHandler.Handle(command, cancellationToken);
        if (token is null)
            return Unauthorized();

        var user = await userManager.FindByNameAsync(request.Username);
        if (user is null)
            return Unauthorized();

        var refreshToken = await refreshTokenService.CreateAsync(user, cancellationToken);
        Response.Cookies.Append(RefreshTokenCookieName, refreshToken, CreateRefreshCookieOptions(DateTimeOffset.UtcNow.Add(RefreshTokenLifetime)));
        return Ok(new AuthTokenResponse(token, user.UserName ?? request.Username));
    }

    [HttpPost("refresh")]
    [AllowAnonymous]
    public async Task<ActionResult<AuthTokenResponse>> Refresh(
        [FromServices] IRefreshTokenService refreshTokenService,
        [FromServices] IConfiguration configuration,
        CancellationToken cancellationToken)
    {
        if (!IsAllowedRefreshOrigin(configuration))
            return Forbid();

        var refreshToken = Request.Cookies[RefreshTokenCookieName];
        if (string.IsNullOrWhiteSpace(refreshToken))
            return Unauthorized();

        var grant = await refreshTokenService.RefreshAsync(refreshToken, cancellationToken);
        if (grant is null)
        {
            DeleteRefreshCookie();
            return Unauthorized();
        }

        Response.Cookies.Append(
            RefreshTokenCookieName,
            grant.RefreshToken,
            CreateRefreshCookieOptions(DateTimeOffset.UtcNow.Add(RefreshTokenLifetime)));
        return Ok(new AuthTokenResponse(grant.AccessToken, grant.Username));
    }

    [HttpPost("logout")]
    [AllowAnonymous]
    public async Task<IActionResult> Logout(
        [FromServices] IRefreshTokenService refreshTokenService,
        [FromServices] IConfiguration configuration,
        CancellationToken cancellationToken)
    {
        if (!IsAllowedRefreshOrigin(configuration))
            return Forbid();

        await refreshTokenService.RevokeAsync(Request.Cookies[RefreshTokenCookieName], cancellationToken);
        DeleteRefreshCookie();
        return NoContent();
    }

    private bool IsAllowedRefreshOrigin(IConfiguration configuration)
    {
        var origin = Request.Headers["Origin"].ToString();
        var allowedOrigins = CorsOriginConfiguration.GetAllowedOrigins(configuration);
        return allowedOrigins.Contains(origin, StringComparer.OrdinalIgnoreCase);
    }

    private static CookieOptions CreateRefreshCookieOptions(DateTimeOffset expires)
    {
        return new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Path = "/Account",
            Expires = expires
        };
    }

    private void DeleteRefreshCookie()
    {
        Response.Cookies.Delete(RefreshTokenCookieName, CreateRefreshCookieOptions(DateTimeOffset.UnixEpoch));
    }
}
