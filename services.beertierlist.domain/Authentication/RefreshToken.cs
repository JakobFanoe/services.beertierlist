namespace services.beertierlist.domain.Authentication;

public sealed class RefreshToken
{
    public Guid Id { get; init; }
    public required string UserId { get; init; }
    public required string TokenHash { get; init; }
    public DateTime CreatedAtUtc { get; init; }
    public DateTime ExpiresAtUtc { get; init; }
    public DateTime? RevokedAtUtc { get; set; }
}
