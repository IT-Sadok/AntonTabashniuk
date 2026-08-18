using SecurityMonitor.Api.Endpoints.Device.Groups.Models;

namespace SecurityMonitor.Api.Endpoints.Device.Groups.Requests;

public sealed record CreateGroupsRequest(
    int DeviceId,
    IReadOnlyList<CreateGroupRequest> Groups
    );