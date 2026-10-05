namespace services.beertierlist.application.Commands.UpdateTierlistEntries;

public interface IUpdateTierlistEntriesCommandHandler
{
    Task<bool> Handle(UpdateTierlistEntriesCommand command, CancellationToken cancellationToken);
}
