using SecurityMonitor.Api.Endpoints.Device.Groups.Zone.Models;
using SecurityMonitor.Api.Endpoints.Device.Groups.Zone.Requests;
using SecurityMonitor.Application.Devices.Groups.Zones.Create;
using SecurityMonitor.Application.Devices.Groups.Zones.Delete;
using SecurityMonitor.Application.Devices.Groups.Zones.Models;
using SecurityMonitor.Application.Devices.Groups.Zones.Update;

namespace SecurityMonitor.Api.Endpoints.Device.Groups.Zone.Mappers;

public static class ZoneMappings
{
    #region Update
    public static UpdateZoneCommand ToCommand(this UpdateZonesRequest request)
    {
        return new UpdateZoneCommand(
            request.DeviceId,
            [.. request.Zones.Select(z => z.ToCommand())]
            );
    }
    public static UpdateZoneModel ToCommand(this UpdateZoneRequest request)
    {
        return new UpdateZoneModel(
            request.ZoneId,
            request.GroupId,
            request.Name,
            request.State,
            request.Type
            );
    }
    #endregion


    #region Create
    public static CreateZoneCommand ToCommand(this CreateZonesRequest request)
    {
        return new CreateZoneCommand(
            request.DeviceId,
            [.. request.Zones.Select(z => z.ToCommand())]
            );
    }
    public static CreateZoneModel ToCommand(this CreateZoneRequest request)
    {
        return new CreateZoneModel(
            request.GroupId,
            request.Name,
            request.State,
            request.Type
            );
    }
    #endregion


    #region Delete
    public static DeleteZoneCommand ToCommand(this DeleteZonesRequest request)
    {
        return new DeleteZoneCommand(
            request.DeviceId,
            request.ZoneIds
            );
    } 
    #endregion
}
