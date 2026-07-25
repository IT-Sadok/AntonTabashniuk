using SecurityMonitor.Application.Devices.Update;

namespace SecurityMonitor.Application.SecuritySchemes.Update;

public sealed record SecuritySchemeUpdateResponse(
    int Id,
    string Name,
    string? Description,
    DeviceUpdateResponce Device);
