using SecurityMonitor.Api.Endpoints.Device.Groups.Zone.Models;

namespace SecurityMonitor.Api.Endpoints.Device.Groups.Zone.Requests;

public record UpdateZonesRequest(
    int DeviceId,
    IReadOnlyList<UpdateZoneRequest> Zones
    );