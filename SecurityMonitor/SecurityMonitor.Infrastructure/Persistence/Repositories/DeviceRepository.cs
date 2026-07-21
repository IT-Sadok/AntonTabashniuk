using SecurityMonitor.Application.Devices;
using SecurityMonitor.Domain.Devices;

namespace SecurityMonitor.Infrastructure.Persistence.Repositories;

internal class DeviceRepository : IDeviceRepository
{
    public Task<int> AddAsync(Device device, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int securitySchemeId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(string name, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<List<Device>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Device?> GetAsync(int securitySchemeId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(Device Device, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
