using SecurityMonitor.Application.Devices.Groups.Zones.Models;

namespace SecurityMonitor.Application.Devices.Groups.Zones.Update;

public record UpdateZonesResponce(
    int DeviceId,
    IReadOnlyList<UpdateZoneModel> Zones
);
