using SecurityMonitor.Domain.Devices.Enums;

namespace SecurityMonitor.Api.Endpoints.Zone.Models;

public record CreateZoneRequest(
    int ZoneId,
    int DeviceId,
    int? GroupId,
    string Name,
    ZoneState State,
    ZoneType Type
    );
