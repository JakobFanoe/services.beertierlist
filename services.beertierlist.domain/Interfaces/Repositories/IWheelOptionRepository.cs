using services.beertierlist.domain.Wheel;

namespace services.beertierlist.domain.Interfaces.Repositories;

public interface IWheelOptionRepository
{
    public Task AddOptions(IReadOnlyList<WheelOption> options, CancellationToken cancellationToken);
    public Task RemoveOption(Guid id, string userId, CancellationToken cancellationToken);
    public Task<IReadOnlyList<WheelOption>> GetOptions(string userId, CancellationToken cancellationToken);
}
