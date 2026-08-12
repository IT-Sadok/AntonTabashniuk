using SecurityMonitor.Application.Devices.Create;
using SecurityMonitor.Application.Devices.Groups.Zones.Mappers;
using SecurityMonitor.Application.Devices.Update;
using SecurityMonitor.Domain.Devices;

namespace SecurityMonitor.Application.Devices.Mappers;

public static class DeviceMapper
{
    public static Device ToDomain(this DeviceUpdateCommand response)
    {
        return new Device(
            response.DeviceId,
            response.SerialNumber,
            response.DeviceType,
            response.DeviceState,
            response.Zones.ToDomain()
        );
    }
    public static Device ToDomain(this DeviceCreateCommand response)
    {
        return new Device(
            0,
            response.SerialNumber,
            response.DeviceType,
            response.DeviceState,
            response.CreateZonesCommand.ToDomain()
        );
    }
    public static DeviceUpdateResponce ToUpdateResponce(this Device response)
    {
        return new DeviceUpdateResponce(
            response.Id,
            response.SerialNumber,
            response.DeviceType,
            response.DeviceState,
            response.Zones.ToUpdateResponce(response.Id)
        );
    }

}
