namespace SecurityMonitor.Application.Devices.Groups.Delete;

public sealed record DeleteGroupsCommand(
    int DeviceId,
    IReadOnlyList<int> ZoneIds
    );
