namespace SecurityMonitor.Application.SecuritySchemes.Update;

public sealed record UpdateResponse(
    int Id,
    string Name,
    string? Description);
