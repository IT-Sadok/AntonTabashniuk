using SecurityMonitor.Api.Endpoints.Zone.Models;

namespace SecurityMonitor.Api.Endpoints.Zone.Requests;

public record CreateZonesRequest(
    int DeviceId,
    IReadOnlyList<CreateZoneRequest> Zones
    );
