namespace SecurityMonitor.Application.SecuritySchemes.Create;

public sealed record SecuritySchemeCreateCommand(
    int id,
    string Name,
    string? Description);
