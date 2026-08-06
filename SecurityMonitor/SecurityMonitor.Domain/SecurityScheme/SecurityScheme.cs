using SecurityMonitor.Domain.Devices;
using SecurityMonitor.Domain.SecurityScheme;

namespace SecurityMonitor.Domain.Administrative;

public class SecurityScheme
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Device Device { get; set; }
    public Address? Address { get; set; }
    public string? Description { get; set; }
    public List<SecuritySchemeMember>? Members { get; set; }
    public SecurityScheme(
        int id,
        string name,
        string? description,
        Device device)
    {
        Id = id;
        Name = name;
        Description = description;
        Device = device;
    }

    public void InitializeScheme(SecurityScheme security) 
    {
        Name = security.Name;
        Description = security.Description;
        Device?.InitializeDevice(security.Device);
    }

    public void Update(SecurityScheme security)
    {
        Name = security.Name;
        Description = security.Description;
        Device?.Update(security.Device);
    }
}
