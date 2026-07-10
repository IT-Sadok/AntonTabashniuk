namespace SecurityMonitor.Application.SecuritySchemes.Update;

public sealed record SecuritySchemeUpdateCommand(
    int Id,
    string Name,
    string? Description);
