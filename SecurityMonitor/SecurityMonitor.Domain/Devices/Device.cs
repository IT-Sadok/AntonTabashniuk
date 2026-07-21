using SecurityMonitor.Domain.Devices.Enums;
using SecurityMonitor.Domain.Devices.Groups;
using SecurityMonitor.Domain.Devices.Groups.Zones;

namespace SecurityMonitor.Domain.Devices;

public class Device
{
    public int Id { get; set; }
    public required int SecuritySchemeId { get; set; }
    public required string SerialNumber { get; set; }
    public DeviceType DeviceType { get; set; }
    public DeviceState DeviceState { get; set; }
    public List<Group>? Groups { get; set; }
    public required List<Zone> Zones { get; set; }

}
