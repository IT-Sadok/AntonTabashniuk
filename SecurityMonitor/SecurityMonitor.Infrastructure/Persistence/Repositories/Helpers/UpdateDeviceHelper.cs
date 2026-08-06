using SecurityMonitor.Domain.Devices;
using SecurityMonitor.Infrastructure.Persistence.Entities;

namespace SecurityMonitor.Infrastructure.Persistence.Repositories.Helpers;

public static class UpdateDeviceHelper 
{
    public static bool UpdateDevice(Device device, DeviceEntity deviceEntity, ApplicationDbContext _dbContext)
    {
        if (deviceEntity is null)
            return false;

        deviceEntity.SecuritySchemeId = device.SecuritySchemeId;
        deviceEntity.SerialNumber = device.SerialNumber;
        deviceEntity.DeviceType = device.DeviceType;
        deviceEntity.DeviceState = device.DeviceState;

        if (deviceEntity.Zones is null || deviceEntity.Zones.Count == 0)
        {
            return false;
        }

        return UpdateZonesHelper.UpdateZones(device.Zones, deviceEntity.Zones, _dbContext);
    }
}
