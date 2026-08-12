using SecurityMonitor.Domain.Administrative;

namespace SecurityMonitor.Infrastructure.Persistence.Entities;

public class SecuritySchemeEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public DeviceEntity Device { get; set; } = null!;
}
