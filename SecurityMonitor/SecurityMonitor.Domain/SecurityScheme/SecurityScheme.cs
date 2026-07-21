using SecurityMonitor.Domain.Devices;
using SecurityMonitor.Domain.SecurityScheme;

namespace SecurityMonitor.Domain.Administrative;

public class SecurityScheme
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Device? Device { get; set; }
    public Address? Address { get; set; }
    public string? Description { get; set; }
    public List<SecuritySchemeMember>? Members { get; set; }
    public SecurityScheme(
        int id,
        string name,
        string? description)
    {
        Id = id;
        Name = name;
        Description = description;
    }
    public void Update(string name, string description)
    {
        Name = name;
        Description = description;
    }
}
