using services.beertierlist.domain.Interfaces.Repositories;
using services.beertierlist.domain.Wheel;

namespace services.beertierlist.application.Queries.WheelOptions;

public class WheelOptionsQueryHandler(IWheelOptionRepository wheelOptionRepository) : IWheelOptionsQueryHandler
{
    public Task<IReadOnlyList<WheelOption>> Handle(WheelOptionsQuery query, CancellationToken cancellationToken)
    {
        return wheelOptionRepository.GetOptions(query.UserId, cancellationToken);
    }
}
