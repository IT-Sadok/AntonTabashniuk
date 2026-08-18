namespace SecurityMonitor.Api.Endpoints.Device.Groups.Zone.Requests;

public record DeleteZonesRequest(
    int DeviceId,
    IReadOnlyList<int> ZoneIds
    );
