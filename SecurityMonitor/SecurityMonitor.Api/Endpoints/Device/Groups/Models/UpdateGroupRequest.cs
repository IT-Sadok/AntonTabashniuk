using SecurityMonitor.Domain.Devices.Enums;

namespace SecurityMonitor.Api.Endpoints.Device.Groups.Models;

public sealed record UpdateGroupRequest(
    int DeviceId,
    int GroupId,
    string Name,
    GroupState State,
    IReadOnlyList<int> ZonesIds
    );
