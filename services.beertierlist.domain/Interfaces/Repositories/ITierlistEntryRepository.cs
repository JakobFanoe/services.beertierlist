using services.beertierlist.domain.Tierlist;

namespace services.beertierlist.domain.Interfaces.Repositories;

public interface ITierlistEntryRepository
{
    public Task AddTierlistEntry(string userId, TierlistEntry tierlistEntry, CancellationToken cancellationToken);
    public Task RemoveTierlistEntry(string userId, Guid tierlistEntryId, CancellationToken cancellationToken);
    public Task<IReadOnlyList<TierlistEntry>> GetEntries(string userId, CancellationToken cancellationToken);
}
