using SecurityMonitor.Domain.Devices.Enums;

namespace SecurityMonitor.Domain.Devices;

public class Device
{
    public int Id { get; set; }
    public int SerialNumber { get; set; }
    public DeviceType DeviceType { get; set; }
    public DeviceState DeviceState { get; set; }
    public List<Group>? Groups { get; set; }
    public required List<Zone> Zones { get; set; }

}
