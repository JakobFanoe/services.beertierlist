namespace services.beertierlist.application.Commands.RegisterAccount;

public interface IRegisterAccountCommandHandler
{
    Task<RegisterAccountResult> Handle(RegisterAccountCommand command, CancellationToken cancellationToken);
}