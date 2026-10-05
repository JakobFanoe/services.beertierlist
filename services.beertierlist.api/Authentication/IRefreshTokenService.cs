using Microsoft.AspNetCore.Identity;

namespace services.beertierlist.api.Authentication;

public interface IRefreshTokenService
{
    Task<string> CreateAsync(IdentityUser user, CancellationToken cancellationToken);
    Task<RefreshTokenGrant?> RefreshAsync(string token, CancellationToken cancellationToken);
    Task RevokeAsync(string? token, CancellationToken cancellationToken);
}

public record RefreshTokenGrant(string AccessToken, string RefreshToken, string Username);
