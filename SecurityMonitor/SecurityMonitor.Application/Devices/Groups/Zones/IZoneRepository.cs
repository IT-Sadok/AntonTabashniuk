using SecurityMonitor.Domain.Devices.Groups.Zones;

namespace SecurityMonitor.Application.Devices.Groups.Zones;

public interface IZoneRepository
{
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken);
    Task<List<Zone>> AddRangeAsync(List<Zone> zones, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(IReadOnlyList<int> zonesIds, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(List<Zone> zones, CancellationToken cancellationToken);
    Task<Zone?> GetAsync(int zone, CancellationToken cancellationToken);
    Task<List<Zone>> GetAllAsync(IReadOnlyList<int> zones, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
