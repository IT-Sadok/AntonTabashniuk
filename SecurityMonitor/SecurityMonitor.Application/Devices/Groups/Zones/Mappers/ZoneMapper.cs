using SecurityMonitor.Application.Devices.Groups.Zones.Create;
using SecurityMonitor.Application.Devices.Groups.Zones.Models;
using SecurityMonitor.Application.Devices.Groups.Zones.Update;
using SecurityMonitor.Domain.Devices.Groups.Zones;

namespace SecurityMonitor.Application.Devices.Groups.Zones.Mappers;

public static class ZoneMapper
{
    #region Create
    public static CreateZonesResponce ToCreateResponce(this List<Zone> zones, int deviceId)
    {
        return new CreateZonesResponce(
            deviceId,
            zones.Select(z => z.ToCreateResponce()).ToList()
        );
    }
    public static CreateZoneModel ToCreateResponce(this Zone zone)
    {
        return new CreateZoneModel(
            zone.GroupId,
            zone.Name,
            zone.State,
            zone.Type
            );
    }

    public static List<Zone> ToDomain(this CreateZoneCommand command)
    {
        return [.. command.Zones.Select(z => z.ToDomain(command.DeviceId)).ToList()];
    }
    public static Zone ToDomain(this CreateZoneModel zone, int deviceId)
    {
        return new Zone(
            0,
            deviceId,
            zone.GroupId,
            zone.Name,
            zone.State,
            zone.Type
            );
    }
    #endregion

    #region Update
    public static UpdateZonesResponce ToUpdateResponce(this List<Zone> zones, int deviceId)
    {
        return new UpdateZonesResponce(
            deviceId,
            zones.Select(z => z.ToUpdateResponce()).ToList()
        );
    }
    public static UpdateZoneModel ToUpdateResponce(this Zone zone)
    {
        return new UpdateZoneModel(
            zone.Id,
            zone.GroupId,
            zone.Name,
            zone.State,
            zone.Type
            );
    }
    public static List<Zone> ToDomain(this UpdateZoneCommand command)
    {
        return [.. command.Zones.Select(z => z.ToDomain(command.DeviceId)).ToList()];
    }
    public static Zone ToDomain(this UpdateZoneModel zone, int deviceId)
    {
        return new Zone(
            zone.ZoneId,
            deviceId,
            zone.GroupId,
            zone.Name,
            zone.State,
            zone.Type
            );
    }
    #endregion
}
