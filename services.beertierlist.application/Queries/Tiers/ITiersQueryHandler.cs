using services.beertierlist.domain.Tierlist;

namespace services.beertierlist.application.Queries.Tiers;

public interface ITiersQueryHandler
{
    public Task<IReadOnlyList<Tier>> Handle(TiersQuery query, CancellationToken cancellationToken);
}
