using services.beertierlist.domain.Tierlist;

namespace services.beertierlist.domain.Interfaces.Repositories;

public interface ITierlistEntryRepository
{
    Task AddTierlistEntry(string userId, TierlistEntry tierlistEntry, CancellationToken cancellationToken);
    Task RemoveTierlistEntry(string userId, Guid tierlistEntryId, CancellationToken cancellationToken);
    Task<IReadOnlyList<TierlistEntry>> GetEntries(string userId, CancellationToken cancellationToken);
    Task<TierlistEntry?> GetEntry(Guid id, string userId, CancellationToken cancellationToken);
    Task<bool> UpdateEntries(string userId, IReadOnlyList<TierlistEntry> updates, CancellationToken cancellationToken);
}
