namespace SecurityMonitor.Application.SecuritySchemes.Create;

public sealed record CreateCommand(
    string Name,
    string? Description);
