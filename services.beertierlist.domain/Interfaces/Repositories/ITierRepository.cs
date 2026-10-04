using services.beertierlist.domain.Tierlist;

namespace services.beertierlist.domain.Interfaces.Repositories;

public interface ITierRepository
{
    public Task AddTier(Tier tier, CancellationToken cancellationToken);
    public Task RemoveTier(string userId, Guid id, CancellationToken cancellationToken);
    public Task<IReadOnlyList<Tier>> GetTiers(string userId, CancellationToken cancellationToken);
}
