namespace services.beertierlist.application.Commands.AddTierlistEntry;

public interface IAddTierlistEntryCommandHandler
{
    Task Handle(AddTierlistEntryCommand command, CancellationToken cancellationToken);
}
