using SecurityMonitor.Domain.Devices.Groups.Zones;
using SecurityMonitor.Infrastructure.Persistence.Entities;

namespace SecurityMonitor.Infrastructure.Persistence.Repositories.Helpers;

public static class UpdateZonesHelper
{
    public static bool UpdateZones(List<Zone> zones, List<ZoneEntity> zoneEntities)
    {
        if (zoneEntities is null || zoneEntities.Count == 0)
            return false;

        foreach (var zoneForUpdate in zoneEntities)
        {
            var zone = zones.FirstOrDefault(x => x.Id == zoneForUpdate.Id);
            if (zone is null)
            {
                return false;
            }
            zoneForUpdate.Name = zone?.Name ?? zoneForUpdate.Name;
            zoneForUpdate.Type = zone?.Type ?? zoneForUpdate.Type;
            zoneForUpdate.State = zone?.State ?? zoneForUpdate.State;
            zoneForUpdate.GroupId = zone?.GroupId ?? zoneForUpdate.GroupId;
        }

        return true;
    }
}
