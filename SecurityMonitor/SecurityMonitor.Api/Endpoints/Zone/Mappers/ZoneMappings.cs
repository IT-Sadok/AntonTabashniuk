using SecurityMonitor.Application.Devices.Groups.Zones.Update;

namespace SecurityMonitor.Api.Endpoints.Zone.Mappers;

public static class ZoneMappings
{
    public static List<UpdateZoneCommand> ToCommand(this List<UpdateZoneRequest> model)
    {
        return [.. model.Select(x => x.ToCommand()).ToList()];
    }
    public static UpdateZoneCommand ToCommand(this UpdateZoneRequest model)
    {
        return new UpdateZoneCommand(
            model.Id,
            model.GroupId,
            model.Name,
            model.State,
            model.Type
            );
    }
}
