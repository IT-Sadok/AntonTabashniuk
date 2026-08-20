using SecurityMonitor.Domain.Devices.Enums;

namespace SecurityMonitor.Application.Devices.Groups.Zones.Models;

public record CreateGroupModel(
    int DeviceId,
    int GroupId,
    string Name,
    GroupState State,
    IReadOnlyList<int> ZonesIds
    );