using SecurityMonitor.Domain.Devices.Enums;
using SecurityMonitor.Domain.Devices.Groups;
using SecurityMonitor.Domain.Devices.Groups.Zones;

namespace SecurityMonitor.Domain.Devices;

public class Device
{
    public int Id { get; set; }
    public int SecuritySchemeId { get; set; } = 0;
    public string SerialNumber { get; set; } = string.Empty;
    public DeviceType DeviceType { get; set; }
    public DeviceState DeviceState { get; set; }
    public List<Group>? Groups { get; set; } = [];
    public List<Zone> Zones { get; set; } = [];

    public Device(){}

    public Device(
        int id,
        string serialNumber,
        DeviceType deviceType,
        DeviceState deviceState,
        List<Zone> zones
        )
    {
        Id = id;
        SerialNumber = serialNumber;
        DeviceType = deviceType;
        DeviceState = deviceState;
        Zones = zones;
    }
    public void InitializeDevice(Device device) 
    {
        SerialNumber = device.SerialNumber;
        DeviceType = device.DeviceType;
        DeviceState = device.DeviceState;
        InitializeZones(device.Zones);
    }
    private void InitializeZones(List<Zone> zones)
    {
        if (zones is null || zones.Count == 0) 
            return;

        Zones.Clear();
        
        foreach (var currentZone in zones)
        {
            Zones.Add(currentZone);
        }
    }
    public void Update(Device device)
    {
        SerialNumber = device.SerialNumber;
        DeviceType = device.DeviceType;
        DeviceState = device.DeviceState;
        UpdateZones(device.Zones);
    }
    private void UpdateZones(List<Zone> zones)
    {
        foreach (var currentZone in Zones)
        {
            var updatedZone = zones.FirstOrDefault(z => z.Id == currentZone.Id);

            if (updatedZone is not null)
            {
                currentZone.Update(updatedZone);
            }
        }
    }
}
