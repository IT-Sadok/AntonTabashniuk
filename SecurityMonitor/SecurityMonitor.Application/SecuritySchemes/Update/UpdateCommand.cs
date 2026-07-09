namespace SecurityMonitor.Application.SecuritySchemes.Update;

public sealed record UpdateCommand(
    int Id,
    string Name,
    string? Description);
