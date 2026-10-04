namespace services.beertierlist.application.Commands.RemoveWheelOption;

public record RemoveWheelOptionCommand(Guid WheelOptionId, string UserId);