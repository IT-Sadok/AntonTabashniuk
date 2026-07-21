using SecurityMonitor.Domain.Devices;

namespace SecurityMonitor.Application.Devices;

public interface IDeviceRepository
{
    Task<bool> ExistsAsync(string name, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken);
    Task<int> AddAsync(Device device, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int securitySchemeId, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(Device Device, CancellationToken cancellationToken);
    Task<Device?> GetAsync(int securitySchemeId, CancellationToken cancellationToken);
    Task<List<Device>> GetAllAsync(CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
