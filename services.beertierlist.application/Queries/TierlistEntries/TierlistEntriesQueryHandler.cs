using services.beertierlist.application.Services;
using services.beertierlist.domain.Interfaces.Repositories;
using services.beertierlist.domain.Tierlist;

namespace services.beertierlist.application.Queries.TierlistEntries;

public class TierlistEntriesQueryHandler(ITierlistEntryRepository tierlistEntryRepository, IImageService imageService) : ITierlistEntriesQueryHandler
{
    public async Task<IReadOnlyList<TierlistEntry>> Handle(TierlistEntriesQuery query, CancellationToken cancellationToken)
    {
        var entries = await tierlistEntryRepository.GetEntries(query.UserId, cancellationToken);
        return entries
            .Select(entry => entry with { ImageUri = imageService.GenerateReadSasUri(entry.Name) })
            .ToArray();
    }
}
