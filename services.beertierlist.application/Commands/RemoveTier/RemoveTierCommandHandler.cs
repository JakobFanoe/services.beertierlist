using services.beertierlist.domain.Interfaces.Repositories;

namespace services.beertierlist.application.Commands.RemoveTier;

public class RemoveTierCommandHandler(ITierRepository tierRepository) : IRemoveTierCommandHandler
{
    public Task Handle(RemoveTierCommand command, CancellationToken cancellationToken)
    {
        return tierRepository.RemoveTier(command.UserId, command.Id, cancellationToken);
    }
}
