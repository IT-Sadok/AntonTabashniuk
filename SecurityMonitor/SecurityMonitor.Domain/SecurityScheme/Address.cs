namespace SecurityMonitor.Domain.SecurityScheme;

public class Address
{
    public int Id { get; set; }
    public required string Settlement { get; set; }
    public required string Region { get; set; }
    public required string Street { get; set; }
    public required string BuildingNumber { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
}
