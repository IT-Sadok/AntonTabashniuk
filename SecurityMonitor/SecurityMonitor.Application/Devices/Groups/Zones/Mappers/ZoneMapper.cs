using SecurityMonitor.Application.Devices.Groups.Zones.Update;
using SecurityMonitor.Domain.Devices.Groups.Zones;

namespace SecurityMonitor.Application.Devices.Groups.Zones.Mappers;

public static class ZoneMapper
{
    public static List<UpdateZoneResponce> ToResponce (this List<Zone> zones) 
    {
        return new List<UpdateZoneResponce> (zones.Select(z => z.ToResponce()).ToList());
    }

    public static UpdateZoneResponce ToResponce(this Zone zone)
    {
        return new UpdateZoneResponce(
            zone.Id,
            zone.GroupId,
            zone.Name,
            zone.State,
            zone.Type
            );
    }

    public static List<Zone> ToDomain(this List<UpdateZoneCommand> zones)
    {
        return new List<Zone>(zones.Select(z => z.ToDomain()).ToList());
    }

    public static Zone ToDomain(this UpdateZoneCommand zone)
    {
        return new Zone(
            zone.Id,
            zone.GroupId,
            zone.Name,
            zone.State,
            zone.Type
            );
    }
}
