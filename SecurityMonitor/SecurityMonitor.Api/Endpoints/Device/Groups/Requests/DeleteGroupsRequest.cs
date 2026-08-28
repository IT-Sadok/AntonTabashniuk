namespace SecurityMonitor.Api.Endpoints.Device.Groups.Requests;

public sealed record DeleteGroupsRequest(
    int DeviceId,
    IReadOnlyList<int> GroupIds
    );