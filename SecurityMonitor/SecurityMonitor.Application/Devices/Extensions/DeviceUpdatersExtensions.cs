using SecurityMonitor.Domain.Devices;
using SecurityMonitor.Domain.Devices.Groups.Zones;

namespace SecurityMonitor.Application.Devices.Extensions;

public static class DeviceUpdatersExtensions
{
    public static void InitializeDevice(this Device device, Device deviceToUpdate)
    {
        device.SerialNumber = device.SerialNumber;
        device.DeviceType = device.DeviceType;
        device.DeviceState = device.DeviceState;
        InitializeZones(device.Zones, deviceToUpdate.Zones);  
    }

    private static void InitializeZones(List<Zone> zones, List<Zone> zonesToUpdate)
    {
        if (zones is null || zones.Count == 0)
            return;

        zones.Clear();

        foreach (var currentZone in zonesToUpdate)
        {
            zones.Add(currentZone);
        }
    }
}
