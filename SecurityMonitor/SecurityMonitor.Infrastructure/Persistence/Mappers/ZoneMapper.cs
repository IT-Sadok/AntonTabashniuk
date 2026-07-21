using SecurityMonitor.Domain.Devices.Groups.Zones;
using SecurityMonitor.Infrastructure.Persistence.Entities;

namespace SecurityMonitor.Infrastructure.Persistence.Mappers;

public static class ZoneMapper
{
    public static ZoneEntity ToEntity(this Zone model)
    {
        return new ZoneEntity
        {
            Id = model.Id,
            GroupId = model.GroupId,
            Name = model.Name,
            State = model.State,
            Type = model.Type,
        };
    }
    public static List<ZoneEntity> ToEntity(this List<Zone> models)
    {
        return models.Select(x => x.ToEntity()).ToList();
    }

    public static Zone ToDomain(this ZoneEntity entity)
    {
        return new Zone
        {
            Id = entity.Id,
            GroupId = entity.GroupId,
            Name = entity.Name,
            State = entity.State,
            Type = entity.Type,
        };
    }
    public static List<Zone> ToDomain(this List<ZoneEntity> entity)
    {
        return entity.Select(x => x.ToDomain()).ToList();
    }
}
