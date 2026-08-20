using SecurityMonitor.Application.Devices.Groups.Zones.Models;

namespace SecurityMonitor.Application.Devices.Groups.Update;

public sealed record UpdateGroupsResponse(
    int DeviceId,
    IReadOnlyList<UpdateGroupModel> Groups
    );
