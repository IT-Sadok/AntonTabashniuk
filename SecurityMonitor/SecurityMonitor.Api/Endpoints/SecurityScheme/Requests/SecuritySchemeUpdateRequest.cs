
namespace SecurityMonitor.Api.Endpoints.SecurityScheme.Requests;

public sealed record SecuritySchemeUpdateRequest(
    string Name,
    string? Description);
