using services.beertierlist.domain.Interfaces.Repositories;

namespace services.beertierlist.application.Commands.UpdateTierlistEntries;

public class UpdateTierlistEntriesCommandHandler(ITierlistEntryRepository tierlistEntryRepository) : IUpdateTierlistEntriesCommandHandler
{
    public Task<bool> Handle(UpdateTierlistEntriesCommand command, CancellationToken cancellationToken)
    {
        return tierlistEntryRepository.UpdateEntries(command.UserId, command.Updates, cancellationToken);
    }
}
