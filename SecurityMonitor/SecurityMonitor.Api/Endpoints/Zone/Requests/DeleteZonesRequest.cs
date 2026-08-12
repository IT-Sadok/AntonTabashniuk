namespace SecurityMonitor.Api.Endpoints.Zone.Requests;

public record DeleteZonesRequest(
    int DeviceId,
    IReadOnlyList<int> ZoneIds
    );
