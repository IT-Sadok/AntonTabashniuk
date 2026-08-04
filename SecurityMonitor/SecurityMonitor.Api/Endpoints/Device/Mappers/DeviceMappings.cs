using SecurityMonitor.Api.Endpoints.Device.Requests;
using SecurityMonitor.Api.Endpoints.Zone.Mappers;
using SecurityMonitor.Application.Devices.Update;

namespace SecurityMonitor.Api.Endpoints.Device.Mappers;

public static class DeviceMappings
{
    public static DeviceUpdateCommand ToCommand(this DeviceUpdateRequest model)
    {
        return new DeviceUpdateCommand(
            model.DeviceId,
            model.SerialNumber,
            model.DeviceType,
            model.DeviceState,
            model.Zones.ToCommand()
            );
    }
}
