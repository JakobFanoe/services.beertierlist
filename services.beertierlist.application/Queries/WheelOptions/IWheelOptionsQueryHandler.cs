using services.beertierlist.domain.Wheel;

namespace services.beertierlist.application.Queries.WheelOptions;

public interface IWheelOptionsQueryHandler
{
    Task<IReadOnlyList<WheelOption>> Handle(WheelOptionsQuery query, CancellationToken cancellationToken);
}
