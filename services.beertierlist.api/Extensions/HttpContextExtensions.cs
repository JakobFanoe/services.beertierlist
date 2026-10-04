using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace services.beertierlist.api.Extensions;

public static class HttpContextExtensions
{
    public static string? GetUserId(this HttpContext httpContext)
    {
        if (httpContext.User.Identity?.IsAuthenticated != true)
            return null;

        var userId = httpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub)
            ?? httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        return string.IsNullOrWhiteSpace(userId) ? null : userId;
    }
}
