using SecurityMonitor.Domain.Devices.Groups;
using SecurityMonitor.Infrastructure.Persistence.Entities;

namespace SecurityMonitor.Infrastructure.Persistence.Mappers;

public static class GroupMapper
{
    public static List<GroupEntity> ToEntity(this List<Group> model)
    {
        return model.Select(x => x.ToEntity()).ToList();
    }
    public static GroupEntity ToEntity(this Group model)
    {
        return new GroupEntity
        {
            Id = model.Id,
            DeviceId = model.DeviceId,
            State = model.State,
            Name = model.Name,
        };
    }
    public static List<Group> ToDomain(this List<GroupEntity> entity)
    {
        return entity.Select(x => x.ToDomain()).ToList();
    }

    public static Group ToDomain(this GroupEntity entity)
    {
        return new Group
        (
            entity.Id,
            entity.DeviceId,
            entity.Name,
            entity.State,   
            entity.Zones.ToDomain()
        );
    }
}
