using SecurityMonitor.Domain.Devices.Enums;

namespace SecurityMonitor.Api.Endpoints.Device.Groups.Models;

public sealed record CreateGroupRequest(
    int DeviceId,
    string Name,
    GroupState State,
    IReadOnlyList<int> ZonesIds
    );