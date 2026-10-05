using services.beertierlist.application.Services;
using services.beertierlist.domain.Interfaces.Repositories;
using services.beertierlist.domain.Tierlist;

namespace services.beertierlist.application.Commands.AddTierlistEntry;

public class AddTierlistEntryCommandHandler(IImageService imageService, ITierlistEntryRepository tierlistEntryRepository) : IAddTierlistEntryCommandHandler
{
    public async Task Handle(AddTierlistEntryCommand command, CancellationToken cancellationToken)
    {
        var uploadedImage = await imageService.UploadImage(command.Content, command.FileName, command.ContentType, cancellationToken);

        await tierlistEntryRepository.AddTierlistEntry(
            command.UserId,
            new TierlistEntry(
                Guid.CreateVersion7(),
                command.UserId,
                TierId: null,
                uploadedImage.Uri,
                uploadedImage.BlobName),
            cancellationToken);
    }
}
