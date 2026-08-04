using SecurityMonitor.Application.Devices.Update;

namespace SecurityMonitor.Application.SecuritySchemes.Get;

public sealed record SecuritySchemeGetResponse(
    int Id,
    string Name,
    string? Description,
    DeviceUpdateResponce Device);