namespace services.beertierlist.application.Commands.RemoveTierlistEntry;

public interface IRemoveTierlistEntryCommandHandler
{
    Task Handle(RemoveTierlistEntryCommand command, CancellationToken cancellationToken);
}
