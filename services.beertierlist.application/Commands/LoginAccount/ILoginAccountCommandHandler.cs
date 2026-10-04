namespace services.beertierlist.application.Commands.LoginAccount;

public interface ILoginAccountCommandHandler
{
    Task<string?> Handle(LoginAccountCommand command, CancellationToken cancellationToken);
}