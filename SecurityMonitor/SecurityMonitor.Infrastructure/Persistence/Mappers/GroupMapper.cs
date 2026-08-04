using SecurityMonitor.Domain.Devices.Groups;
using SecurityMonitor.Infrastructure.Persistence.Entities;

namespace SecurityMonitor.Infrastructure.Persistence.Mappers;

public static class GroupMapper
{
    public static GroupEntity ToEntity(this Group model)
    {
        return new GroupEntity
        {
            Id = model.Id,
            State = model.State,
            Name = model.Name,
            Zones = model.Zones.Select(x => x.ToEntity()).ToList()
        };
    }

    public static List<GroupEntity> ToEntity(this List<Group> model)
    {
        return model.Select(x => x.ToEntity()).ToList();
    }

    public static Group ToDomain(this GroupEntity entity)
    {
        return new Group
        {
            Id = entity.Id,
            Zones = entity.Zones.ToDomain(),
            Name = entity.Name,
            State = entity.State,   
        };
    }

    public static List<Group> ToDomain(this List<GroupEntity> entity)
    {
        return entity.Select(x => x.ToDomain()).ToList();
    }
}
