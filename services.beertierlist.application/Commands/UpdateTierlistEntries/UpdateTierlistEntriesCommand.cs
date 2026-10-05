using services.beertierlist.domain.Tierlist;

namespace services.beertierlist.application.Commands.UpdateTierlistEntries;

public record UpdateTierlistEntriesCommand(string UserId, IReadOnlyList<TierlistEntry> Updates);
