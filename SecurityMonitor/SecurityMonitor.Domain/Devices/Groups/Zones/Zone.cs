using SecurityMonitor.Domain.Devices.Enums;

namespace SecurityMonitor.Domain.Devices.Groups.Zones;

public class Zone
{
    public int Id { get; set; }
    public int? GroupId { get; set; }
    public string Name { get; set; }
    public ZoneState State { get; set; }
    public ZoneType Type { get; set; }

    public Zone(
        int id,
        int? groupId,
        string name,
        ZoneState state,
        ZoneType type) 
    {
        Id = id;
        GroupId = groupId;
        Name = name;
        State = state;
        Type = type;
    }
    public void Update(Zone zone)
    {
        Name = zone.Name;
        GroupId = zone.Id;
        State = zone.State;
        Type = zone.Type;
    }
}
