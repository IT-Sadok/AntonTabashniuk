using SecurityMonitor.Domain.Devices.Enums;

namespace SecurityMonitor.Domain.Devices;

public class Zone
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public ZoneState State { get; set; }
    public ZoneType Type { get; set; }
}
