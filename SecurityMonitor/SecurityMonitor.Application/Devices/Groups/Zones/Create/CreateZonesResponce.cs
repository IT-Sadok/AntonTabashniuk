using SecurityMonitor.Application.Devices.Groups.Zones.Models;

namespace SecurityMonitor.Application.Devices.Groups.Zones.Create;

public record CreateZonesResponce(
    int DeviceId,
    IReadOnlyList<CreateZoneModel> Zones
);
