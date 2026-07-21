using SecurityMonitor.Domain.Devices.Enums;

namespace SecurityMonitor.Infrastructure.Persistence.Entities;

public class GroupEntity
{
    public int Id { get; set; }

    public int DeviceId { get; set; }
    public DeviceEntity Device { get; set; } = null!;
    
    public required string Name { get; set; }
    public GroupState State { get; set; }

    public List<ZoneEntity> Zones { get; set; } = [];
}
