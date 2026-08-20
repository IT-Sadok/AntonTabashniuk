using SecurityMonitor.Application.Devices.Groups.Zones.Models;

namespace SecurityMonitor.Application.Devices.Groups.Create;

public sealed record CreateGroupsResponse(
    int DeviceId,
    IReadOnlyList<CreateGroupModel> Groups
    );
