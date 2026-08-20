using SecurityMonitor.Domain.Devices.Enums;

namespace SecurityMonitor.Domain.Devices.Groups.Zones;

public class Zone
{
    public int Id { get; set; }
    public int DeviceId { get; set; }
    public int? GroupId { get; set; }
    public string Name { get; set; }
    public ZoneState State { get; set; }
    public ZoneType Type { get; set; }

    public Zone(int id)
    {
        Id = id;
    }

    public Zone(
        int id,
        int deviceId,
        int? groupId,
        string name,
        ZoneState state,
        ZoneType type) 
    {
        Id = id;
        DeviceId = deviceId;
        GroupId = groupId;
        Name = name;
        State = state;
        Type = type;
    }
}
