using SecurityMonitor.Domain.Devices;
using SecurityMonitor.Infrastructure.Persistence.Entities;

namespace SecurityMonitor.Infrastructure.Persistence.Mappers;

public static class DeviceMapper
{
    public static DeviceEntity ToEntity(this Device model)
    {
        return new DeviceEntity
        {
            Id = model.Id,
            SerialNumber = model.SerialNumber,
            SecuritySchemeId = model.SecuritySchemeId,
            DeviceState = model.DeviceState,
            DeviceType = model.DeviceType,
            Groups = model.Groups?.ToEntity(),
            Zones = model.Zones.ToEntity(),
        };
    }

    public static Device ToDomain(this DeviceEntity entity)
    {
        return new Device 
        {
            Id = entity.Id,
            SerialNumber = entity.SerialNumber,
            SecuritySchemeId = entity.SecuritySchemeId,
            DeviceState = entity.DeviceState,
            DeviceType= entity.DeviceType,
            Groups = entity.Groups?.ToDomain(),
            Zones = entity.Zones.ToDomain(),
        };
    }
}
