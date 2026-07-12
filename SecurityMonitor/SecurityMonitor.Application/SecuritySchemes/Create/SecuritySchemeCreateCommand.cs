namespace SecurityMonitor.Application.SecuritySchemes.Create;

public sealed record SecuritySchemeCreateCommand(
    string Name,
    string? Description);
