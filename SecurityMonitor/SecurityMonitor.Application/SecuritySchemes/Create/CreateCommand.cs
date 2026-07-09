namespace SecurityMonitor.Application.SecuritySchemes.Create;

public sealed record CreateCommand(
    int id,
    string Name,
    string? Description);
