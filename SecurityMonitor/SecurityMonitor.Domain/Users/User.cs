namespace SecurityMonitor.Domain.Users;

public class User
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public string? LastName { get; set; }
    public string? MiddleName { get; set; }
    public required List<string> Phones { get; set; }

}
