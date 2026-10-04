using services.beertierlist.domain.Interfaces.Repositories;
using services.beertierlist.domain.Wheel;

namespace services.beertierlist.application.Commands.AddWheelOptions;

public class AddWheelOptionsCommandHandler(IWheelOptionRepository wheelOptionRepository) : IAddWheelOptionsCommandHandler
{
    public Task Handle(AddWheelOptionsCommand command, CancellationToken cancellationToken)
    {
        var wheelOptions = command.WheelOptions.Select(option => new WheelOption(Guid.CreateVersion7(), command.UserId, option)).ToList();

        return wheelOptionRepository.AddOptions(wheelOptions, cancellationToken);
    }
}
