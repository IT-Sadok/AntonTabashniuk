using SecurityMonitor.Application.Devices.Groups.Create;
using SecurityMonitor.Application.Devices.Groups.Update;
using SecurityMonitor.Application.Devices.Groups.Zones.Models;
using SecurityMonitor.Domain.Devices.Groups;
using SecurityMonitor.Domain.Devices.Groups.Zones;

namespace SecurityMonitor.Application.Devices.Groups.Mappers;

public static class GroupMappers
{
    #region Create
    public static CreateGroupsResponse ToCreateResponce(this IReadOnlyList<int> Groups, int deviceId)
    {
        return new CreateGroupsResponse(
            deviceId,
            [.. Groups.Select(z => z)]
        );
    }

    public static List<Group> ToDomain(this CreateGroupsCommand command)
    {
        return [.. command.Groups.Select(z => z.ToDomain(command.DeviceId)).ToList()];
    }
    public static Group ToDomain(this CreateGroupModel Group, int deviceId)
    {
        return new Group(
            0,
            deviceId,
            Group.Name,
            Group.State,
            [.. Group.ZonesIds.Select(x=> new Zone(x, deviceId))]
            );
    }
    #endregion

    #region Update
    public static UpdateGroupsResponse ToUpdateResponce(this IReadOnlyList<Group> Groups, int deviceId)
    {
        return new UpdateGroupsResponse(
            deviceId,
            [.. Groups.Select(z => z.ToUpdateResponce())]
        );
    }
    public static UpdateGroupModel ToUpdateResponce(this Group Group)
    {
        return new UpdateGroupModel(
            Group.DeviceId,
            Group.Id,
            Group.Name,
            Group.State,
            [.. Group.Zones.Select(x=>x.Id)]
            );
    }
    public static List<Group> ToDomain(this UpdateGroupsCommand command)
    {
        return [.. command.Groups.Select(z => z.ToDomain(command.DeviceId)).ToList()];
    }
    public static Group ToDomain(this UpdateGroupModel Group, int deviceId)
    {
        return new Group(
            Group.GroupId,
            deviceId,
            Group.Name,
            Group.State,
            [.. Group.Zones.Select(x => new Zone(x, deviceId))]
            );
    }
    #endregion
}
