namespace services.beertierlist.application.Commands.RegisterAccount;

public enum RegisterAccountStatus
{
    Created,
    UsernameUnavailable,
    Failed
}

public record RegisterAccountResult(RegisterAccountStatus Status, string? UserId = null, string? Username = null);