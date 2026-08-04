using SecurityMonitor.Domain.Devices.Enums;
using SecurityMonitor.Domain.Devices.Groups.Zones;

namespace SecurityMonitor.Domain.Devices.Groups;

public class Group
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public GroupState State { get; set; }
    public required List<Zone> Zones { get; set; }

}
