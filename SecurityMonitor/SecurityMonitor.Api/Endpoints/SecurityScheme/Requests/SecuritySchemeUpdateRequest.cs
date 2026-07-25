using SecurityMonitor.Api.Endpoints.Device.Requests;

namespace SecurityMonitor.Api.Endpoints.SecurityScheme.Requests;

public sealed record SecuritySchemeUpdateRequest(
    int Id,
    string Name,
    string? Description,
    DeviceUpdateRequest Device);
