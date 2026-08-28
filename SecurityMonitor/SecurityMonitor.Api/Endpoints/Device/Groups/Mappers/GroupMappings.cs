using SecurityMonitor.Api.Endpoints.Device.Groups.Models;
using SecurityMonitor.Api.Endpoints.Device.Groups.Requests;
using SecurityMonitor.Application.Devices.Groups.Create;
using SecurityMonitor.Application.Devices.Groups.Delete;
using SecurityMonitor.Application.Devices.Groups.Update;
using SecurityMonitor.Application.Devices.Groups.Zones.Models;

namespace SecurityMonitor.Api.Endpoints.Device.Groups.Mappers;

public static class GroupMappings
{
    #region Update
    public static UpdateGroupsCommand ToCommand(this UpdateGroupsRequest request)
    {
        return new UpdateGroupsCommand(
            request.DeviceId,
            [.. request.Groups.Select(z => z.ToCommand(request.DeviceId))]
            );
    }
    public static UpdateGroupModel ToCommand(this UpdateGroupRequest request, int deviceId)
    {
        return new UpdateGroupModel(
            deviceId,
            request.GroupId,
            request.Name,
            request.State,
            request.ZonesIds
            );
    }
    #endregion


    #region Create
    public static CreateGroupsCommand ToCommand(this CreateGroupsRequest request)
    {
        return new CreateGroupsCommand(
            request.DeviceId,
            [.. request.Groups.Select(z => z.ToCommand())]
            );
    }

    public static CreateGroupModel ToCommand(this CreateGroupRequest request)
    {
        return new CreateGroupModel(
            request.DeviceId,
            0,
            request.Name,
            request.State,
            request.ZonesIds
            );
    }
    #endregion


    #region Delete
    public static DeleteGroupsCommand ToCommand(this DeleteGroupsRequest request)
    {
        return new DeleteGroupsCommand(
            request.DeviceId,
            request.GroupIds
            );
    }
    #endregion
}
