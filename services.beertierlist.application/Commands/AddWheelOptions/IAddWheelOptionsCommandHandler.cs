namespace services.beertierlist.application.Commands.AddWheelOptions;

public interface IAddWheelOptionsCommandHandler
{
    Task Handle(AddWheelOptionsCommand command, CancellationToken cancellationToken);
}
