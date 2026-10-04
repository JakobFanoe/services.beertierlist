namespace services.beertierlist.application.Commands.AddTierlistEntry;

public record AddTierlistEntryCommand(Stream Content, string ContentType, string FileName, string UserId);
