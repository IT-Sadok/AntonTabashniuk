namespace SecurityMonitor.Domain.SecurityScheme;

public class Address
{
    public int Id { get; set; }
    public required string Region { get; set; }
    public required string Street { get; set; }
    public required string Settlement { get; set; }
    public required string HouseNumber { get; set; }
    public required string Coordinates { get; set; }
}
