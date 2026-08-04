using SecurityMonitor.Domain.Devices.Enums;

namespace SecurityMonitor.Application.Devices.Groups.Zones.Update;

public record UpdateZoneCommand(
    int Id,
    int? GroupId,
    string Name,
    ZoneState State,
    ZoneType Type
    );