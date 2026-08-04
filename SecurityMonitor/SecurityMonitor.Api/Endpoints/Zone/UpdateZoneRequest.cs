using SecurityMonitor.Domain.Devices.Enums;

namespace SecurityMonitor.Api.Endpoints.Zone;

public record UpdateZoneRequest(
    int Id,
    int? GroupId,
    string Name,
    ZoneState State,
    ZoneType Type
    );