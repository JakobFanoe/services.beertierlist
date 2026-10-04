namespace services.beertierlist.application.Commands.AddWheelOptions;

public record AddWheelOptionsCommand(string UserId, IReadOnlyList<string> WheelOptions);
