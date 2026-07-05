using SecurityMonitor.Domain.Devices.Enums;

namespace SecurityMonitor.Domain.Devices;

public class Group
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public GroupState State { get; set; }
    public required List<Zone> Zones { get; set; }

}
