using SecurityMonitor.Domain.Devices.Enums;
using SecurityMonitor.Domain.Devices.Groups.Zones;

namespace SecurityMonitor.Domain.Devices.Groups;

public class Group
{
    public int DeviceId { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }
    public GroupState State { get; set; }
    public List<Zone> Zones { get; set; }

    public Group(
    int id,
    int deviceId,
    string name,
    GroupState state,
    List<Zone> zones
    )
    {
        Id = id;
        DeviceId = deviceId;
        Name = name;
        State = state;
        Zones = zones;
    }
}
