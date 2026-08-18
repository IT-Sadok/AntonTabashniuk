using SecurityMonitor.Domain.Devices.Enums;
namespace SecurityMonitor.Api.Endpoints.Device.Groups.Zone.Models;

public record UpdateZoneRequest(
    int ZoneId, 
    int DeviceId,
    int? GroupId,
    string Name,
    ZoneState State,
    ZoneType Type
    );