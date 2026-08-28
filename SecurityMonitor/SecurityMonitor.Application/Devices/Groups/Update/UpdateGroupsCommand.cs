using SecurityMonitor.Application.Devices.Groups.Zones.Models;

namespace SecurityMonitor.Application.Devices.Groups.Update;

public sealed record UpdateGroupsCommand(
    int DeviceId,
    IReadOnlyList<UpdateGroupModel> Groups
    );
