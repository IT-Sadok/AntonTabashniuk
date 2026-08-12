using SecurityMonitor.Application.Devices.Groups.Zones.Models;

namespace SecurityMonitor.Application.Devices.Groups.Zones.Create;

public record CreateZoneCommand(
    int DeviceId,
    IReadOnlyList<CreateZoneModel> Zones
    );
