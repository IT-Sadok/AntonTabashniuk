using SecurityMonitor.Api.Endpoints.Device.Groups.Zone.Models;

namespace SecurityMonitor.Api.Endpoints.Device.Groups.Zone.Requests;

public record CreateZonesRequest(
    int DeviceId,
    IReadOnlyList<CreateZoneRequest> Zones
    );
