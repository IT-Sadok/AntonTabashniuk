using SecurityMonitor.Domain.Devices.Groups.Zones;
using SecurityMonitor.Infrastructure.Persistence.Entities;

namespace SecurityMonitor.Infrastructure.Persistence.Repositories.Helpers;

public static class UpdateZonesHelper
{
    public static bool UpdateZones(List<Zone> zones, List<ZoneEntity> zoneEntities, ApplicationDbContext context)
    {
        zoneEntities ??= [];
        var dbDictionary = zoneEntities.ToDictionary(x => x.Id);

        var dictionary = zones.ToDictionary(x => x.Id);

        foreach (var zone in zones)
        {
            if (zone.Id == 0)
            {
                zoneEntities.Add(new ZoneEntity
                {
                    DeviceId = zone.DeviceId,
                    GroupId = zone.GroupId,
                    Name = zone.Name,
                    State = zone.State,
                    Type = zone.Type
                });

                continue;
            }

            if (!dbDictionary.TryGetValue(zone.Id, out var entity))
            {
                return false;
            }

            entity.Name = zone.Name;
            entity.GroupId = zone.GroupId;
            entity.State = zone.State;
            entity.Type = zone.Type;
        }

        var requestIds = zones
            .Where(x => x.Id != 0)
            .Select(x => x.Id)
            .ToHashSet();

        var entitiesToDelete = zoneEntities
            .Where(x => !requestIds.Contains(x.Id))
            .ToList();

        context.RemoveRange(entitiesToDelete);
        
        return true;
    }
}
