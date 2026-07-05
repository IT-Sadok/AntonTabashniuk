using SecurityMonitor.Domain.Administrative.Enums;
using SecurityMonitor.Domain.Users;

namespace SecurityMonitor.Domain.Administrative;

public class SecuritySchemeMember
{
    public int Id { get; set; }
    public required User User { get; set; }
    public SecuritySchemeUserRole Role { get; set; }

}
