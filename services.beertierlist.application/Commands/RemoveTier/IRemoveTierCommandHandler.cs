namespace services.beertierlist.application.Commands.RemoveTier;

public interface IRemoveTierCommandHandler
{
    Task Handle(RemoveTierCommand command, CancellationToken cancellationToken);
}
