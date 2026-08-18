using SecurityMonitor.Api.Endpoints.Device.Groups.Models;

namespace SecurityMonitor.Api.Endpoints.Device.Groups.Requests;

public sealed record UpdateGroupsRequest(
    int DeviceId,
    IReadOnlyList<UpdateGroupRequest> GroupIds
    );
