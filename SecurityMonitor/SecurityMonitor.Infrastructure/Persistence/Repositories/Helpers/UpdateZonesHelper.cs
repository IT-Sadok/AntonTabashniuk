using SecurityMonitor.Domain.Devices.Groups.Zones;
using SecurityMonitor.Infrastructure.Persistence.Entities;

namespace SecurityMonitor.Infrastructure.Persistence.Repositories.Helpers;

public static class UpdateZonesHelper
{
    public static bool UpdateZones(List<Zone> zones, List<ZoneEntity> zoneEntities, ApplicationDbContext context)
    {
        zoneEntities ??= [];

        var dbDictionary = zoneEntities.ToDictionary(x => x.Id);

        // DELETE
        var requestIds = zones
            .Where(x => x.Id != 0)
            .Select(x => x.Id)
            .ToHashSet();

        var entitiesToDelete = zoneEntities
            .Where(x => !requestIds.Contains(x.Id))
            .ToList();

        context.Zones.RemoveRange(entitiesToDelete);

        // UPDATE
        foreach (var zone in zones.Where(x => x.Id != 0))
        {
            if (!dbDictionary.TryGetValue(zone.Id, out var entity))
            {
                return false;
            }

            entity.Name = zone.Name;
            entity.GroupId = null;
            entity.State = zone.State;
            entity.Type = zone.Type;
        }

        // CREATE
        var entitiesToCreate = zones
            .Where(x => x.Id == 0)
            .Select(zone => new ZoneEntity
            {
                DeviceId = zone.DeviceId,
                GroupId = null,
                Name = zone.Name,
                State = zone.State,
                Type = zone.Type
            });

        context.Zones.AddRange(entitiesToCreate);

        return true;
    }
}
