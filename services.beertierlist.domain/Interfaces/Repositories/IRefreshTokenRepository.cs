using services.beertierlist.domain.Authentication;

namespace services.beertierlist.domain.Interfaces.Repositories;

public interface IRefreshTokenRepository
{
    Task Add(RefreshToken refreshToken, CancellationToken cancellationToken);
    Task<RefreshToken?> GetByTokenHash(string tokenHash, CancellationToken cancellationToken);
    Task<bool> Rotate(string tokenHash, RefreshToken replacement, DateTime nowUtc, CancellationToken cancellationToken);
    Task Revoke(string tokenHash, DateTime nowUtc, CancellationToken cancellationToken);
}
