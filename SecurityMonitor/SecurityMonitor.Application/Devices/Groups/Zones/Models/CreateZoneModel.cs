using SecurityMonitor.Domain.Devices.Enums;

namespace SecurityMonitor.Application.Devices.Groups.Zones.Models;

public record CreateZoneModel(
    int? GroupId,
    string Name,
    ZoneState State,
    ZoneType Type
    );