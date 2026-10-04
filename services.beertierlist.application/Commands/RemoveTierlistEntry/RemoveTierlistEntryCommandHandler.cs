using services.beertierlist.application.Services;
using services.beertierlist.domain.Interfaces.Repositories;

namespace services.beertierlist.application.Commands.RemoveTierlistEntry;

public class RemoveTierlistEntryCommandHandler(ITierlistEntryRepository tierlistEntryRepository, IImageService imageService) : IRemoveTierlistEntryCommandHandler
{
    public async Task Handle(RemoveTierlistEntryCommand command, CancellationToken cancellationToken)
    {
        var entry = await tierlistEntryRepository.GetEntry(command.Id, command.UserId, cancellationToken);

        if (entry is null)
        {
            throw new InvalidOperationException($"Entry with id {command.Id} not found for user {command.UserId}");
        }

        await tierlistEntryRepository.RemoveTierlistEntry(command.UserId, command.Id, cancellationToken);
        await imageService.RemoveImage(entry!.Name, cancellationToken);
    }
}
