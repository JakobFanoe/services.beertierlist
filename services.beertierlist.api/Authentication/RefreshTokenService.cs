using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using services.beertierlist.application.Services;
using services.beertierlist.domain.Authentication;
using services.beertierlist.domain.Interfaces.Repositories;

namespace services.beertierlist.api.Authentication;

public class RefreshTokenService(
    IRefreshTokenRepository refreshTokenRepository,
    IAccessTokenGenerator accessTokenGenerator,
    UserManager<IdentityUser> userManager) : IRefreshTokenService
{
    private static readonly TimeSpan TokenLifetime = TimeSpan.FromDays(30);

    public async Task<string> CreateAsync(IdentityUser user, CancellationToken cancellationToken)
    {
        var rawToken = CreateRawToken();
        var now = DateTime.UtcNow;

        await refreshTokenRepository.Add(CreateEntity(user.Id, rawToken, now), cancellationToken);
        return rawToken;
    }

    public async Task<RefreshTokenGrant?> RefreshAsync(string token, CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;
        var tokenHash = HashToken(token);
        var current = await refreshTokenRepository.GetByTokenHash(tokenHash, cancellationToken);

        if (current is null || current.RevokedAtUtc is not null || current.ExpiresAtUtc <= now)
            return null;

        var user = await userManager.FindByIdAsync(current.UserId);
        if (user is null)
        {
            await refreshTokenRepository.Revoke(tokenHash, now, cancellationToken);
            return null;
        }

        var replacementToken = CreateRawToken();
        var rotated = await refreshTokenRepository.Rotate(
            tokenHash,
            CreateEntity(user.Id, replacementToken, now),
            now,
            cancellationToken);

        if (!rotated)
            return null;

        var accessToken = accessTokenGenerator.Generate(user.Id, user.UserName ?? string.Empty);
        return new RefreshTokenGrant(accessToken, replacementToken, user.UserName ?? string.Empty);
    }

    public Task RevokeAsync(string? token, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(token))
            return Task.CompletedTask;

        return refreshTokenRepository.Revoke(HashToken(token), DateTime.UtcNow, cancellationToken);
    }

    private static RefreshToken CreateEntity(string userId, string rawToken, DateTime nowUtc)
    {
        return new RefreshToken
        {
            Id = Guid.CreateVersion7(),
            UserId = userId,
            TokenHash = HashToken(rawToken),
            CreatedAtUtc = nowUtc,
            ExpiresAtUtc = nowUtc.Add(TokenLifetime)
        };
    }

    private static string CreateRawToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64))
            .TrimEnd('=')
            .Replace('+', '-')
            .Replace('/', '_');
    }

    private static string HashToken(string token)
    {
        return Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(token)));
    }
}
