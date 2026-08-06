using SecurityMonitor.Domain.Devices.Groups.Zones;
using SecurityMonitor.Infrastructure.Persistence.Entities;

namespace SecurityMonitor.Infrastructure.Persistence.Repositories.Helpers;

public static class UpdateZonesHelper
{
    public static bool UpdateZones(List<Zone> zones, List<ZoneEntity> zoneEntities)
    {
        if (zoneEntities is null || zoneEntities.Count == 0)
            return false;

        var dictionary = zones.ToDictionary(x => x.Id);

        foreach (var zoneForUpdate in zoneEntities)
        {
            var zone = dictionary[zoneForUpdate.Id];

            if (zone is null)
            {
                return false;
            }

            zoneForUpdate.Name = zoneForUpdate.Name;
            zoneForUpdate.Type = zoneForUpdate.Type;
            zoneForUpdate.State = zoneForUpdate.State;
            zoneForUpdate.GroupId = zoneForUpdate.GroupId;
        }

        return true;
    }
}
