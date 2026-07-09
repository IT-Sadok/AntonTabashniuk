namespace SecurityMonitor.Application.SecuritySchemes.Get;

public sealed record GetResponse(
    int Id,
    string Name,
    string? Description);