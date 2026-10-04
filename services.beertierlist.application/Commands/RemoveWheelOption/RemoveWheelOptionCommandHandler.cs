using services.beertierlist.domain.Interfaces.Repositories;

namespace services.beertierlist.application.Commands.RemoveWheelOption;

public class RemoveWheelOptionCommandHandler(IWheelOptionRepository wheelOptionRepository) : IRemoveWheelOptionCommandHandler
{
    public Task Handle(RemoveWheelOptionCommand command, CancellationToken cancellationToken)
    {
        return wheelOptionRepository.RemoveOption(command.WheelOptionId, command.UserId, cancellationToken);
    }
}
