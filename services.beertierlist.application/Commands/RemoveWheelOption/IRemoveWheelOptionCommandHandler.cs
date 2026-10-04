namespace services.beertierlist.application.Commands.RemoveWheelOption;

public interface IRemoveWheelOptionCommandHandler
{
    Task Handle(RemoveWheelOptionCommand command, CancellationToken cancellationToken);
}
