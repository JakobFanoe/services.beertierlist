namespace services.beertierlist.domain.Tierlist;

public record TierlistEntry(
    Guid Id,
    string UserId,
    string? TierId,
    string ImageUri,
    string Name,
    int? Position = null);
