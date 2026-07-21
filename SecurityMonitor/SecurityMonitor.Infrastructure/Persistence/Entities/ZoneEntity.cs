using SecurityMonitor.Domain.Devices.Enums;

namespace SecurityMonitor.Infrastructure.Persistence.Entities;

public class ZoneEntity
{
    public int Id { get; set; }

    public int DeviceId { get; set; }
    public DeviceEntity Device { get; set; } = null!;
    
    public int? GroupId { get; set; }
    public GroupEntity? Group { get; set; }

    public required string Name { get; set; }

    public ZoneState State { get; set; }
    public ZoneType Type { get; set; }
}
