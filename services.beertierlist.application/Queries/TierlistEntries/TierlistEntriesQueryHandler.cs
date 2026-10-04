using services.beertierlist.domain.Interfaces.Repositories;
using services.beertierlist.domain.Tierlist;

namespace services.beertierlist.application.Queries.TierlistEntries;

public class TierlistEntriesQueryHandler(ITierlistEntryRepository tierlistEntryRepository) : ITierlistEntriesQueryHandler
{
    public Task<IReadOnlyList<TierlistEntry>> Handle(TierlistEntriesQuery query, CancellationToken cancellationToken)
    {
        return tierlistEntryRepository.GetEntries(query.UserId, cancellationToken);
    }
}
