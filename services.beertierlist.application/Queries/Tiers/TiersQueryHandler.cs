using services.beertierlist.domain.Interfaces.Repositories;
using services.beertierlist.domain.Tierlist;

namespace services.beertierlist.application.Queries.Tiers;

public class TiersQueryHandler(ITierRepository tierRepository) : ITiersQueryHandler
{
    public Task<IReadOnlyList<Tier>> Handle(TiersQuery query, CancellationToken cancellationToken)
    {
        return tierRepository.GetTiers(query.UserId, cancellationToken);
    }
}
