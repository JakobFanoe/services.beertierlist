using Microsoft.EntityFrameworkCore;
using services.beertierlist.domain.Authentication;
using services.beertierlist.domain.Interfaces;
using services.beertierlist.domain.Interfaces.Repositories;

namespace services.beertierlist.infrastructure.Repositories;

public class RefreshTokenRepository(IBeertierlistDbContext dbContext) : IRefreshTokenRepository
{
    public async Task Add(RefreshToken refreshToken, CancellationToken cancellationToken)
    {
        dbContext.RefreshTokens.Add(refreshToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<RefreshToken?> GetByTokenHash(string tokenHash, CancellationToken cancellationToken)
    {
        return dbContext.RefreshTokens
            .FirstOrDefaultAsync(token => token.TokenHash == tokenHash, cancellationToken);
    }

    public async Task<bool> Rotate(string tokenHash, RefreshToken replacement, DateTime nowUtc, CancellationToken cancellationToken)
    {
        var current = await dbContext.RefreshTokens
            .FirstOrDefaultAsync(token => token.TokenHash == tokenHash, cancellationToken);

        if (current is null || current.RevokedAtUtc is not null || current.ExpiresAtUtc <= nowUtc)
            return false;

        current.RevokedAtUtc = nowUtc;
        dbContext.RefreshTokens.Add(replacement);
        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task Revoke(string tokenHash, DateTime nowUtc, CancellationToken cancellationToken)
    {
        var token = await dbContext.RefreshTokens
            .FirstOrDefaultAsync(candidate => candidate.TokenHash == tokenHash, cancellationToken);

        if (token is null || token.RevokedAtUtc is not null)
            return;

        token.RevokedAtUtc = nowUtc;
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
