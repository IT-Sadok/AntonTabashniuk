using SecurityMonitor.Domain.Devices;
using SecurityMonitor.Domain.Devices.Enums;
using SecurityMonitor.Domain.Devices.Groups.Zones;
using SecurityMonitor.Infrastructure.Persistence.Mappers;

namespace SecurityMonitor.Infrastructure.Persistence.Entities;

public class DeviceEntity
{
    public int Id { get; set; }
    
    public int SecuritySchemeId { get; set; }
    public SecuritySchemeEntity SecurityScheme { get; set; } = null!;
    
    public string SerialNumber { get; set; } = string.Empty;
    
    public DeviceType DeviceType { get; set; }
    public DeviceState DeviceState { get; set; }

    public List<GroupEntity> Groups { get; set; } = [];
    public List<ZoneEntity> Zones { get; set; } = [];
}
