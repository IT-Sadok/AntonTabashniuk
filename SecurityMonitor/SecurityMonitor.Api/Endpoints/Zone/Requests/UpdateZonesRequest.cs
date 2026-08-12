namespace SecurityMonitor.Api.Endpoints.Zone.Models;

public record UpdateZonesRequest(
    int DeviceId,
    IReadOnlyList<UpdateZoneRequest> Zones
    );