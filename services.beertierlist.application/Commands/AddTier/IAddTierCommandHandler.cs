namespace services.beertierlist.application.Commands.AddTier;

public interface IAddTierCommandHandler
{
    Task Handle(AddTierCommand command, CancellationToken cancellationToken);
}
