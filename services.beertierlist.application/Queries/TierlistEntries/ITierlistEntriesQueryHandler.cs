using services.beertierlist.domain.Tierlist;

namespace services.beertierlist.application.Queries.TierlistEntries;

public interface ITierlistEntriesQueryHandler
{
    Task<IReadOnlyList<TierlistEntry>> Handle(TierlistEntriesQuery query, CancellationToken cancellationToken);
}
