namespace SecurityMonitor.Application.Devices.Groups.Create;

public sealed record CreateGroupsResponse(
    int DeviceId,
    IReadOnlyList<int> Groups
    );
