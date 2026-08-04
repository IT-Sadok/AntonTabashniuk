using Microsoft.EntityFrameworkCore;
using SecurityMonitor.Application.Devices;
using SecurityMonitor.Domain.Devices;
using SecurityMonitor.Infrastructure.Persistence.Mappers;
using System.Xml.Linq;

namespace SecurityMonitor.Infrastructure.Persistence.Repositories;

public class DeviceRepository : IDeviceRepository
{
    private readonly ApplicationDbContext _dbContext;
    public DeviceRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
                   
    public async Task<Device?> GetAsync(int deviceId, CancellationToken cancellationToken)
    {
        return await _dbContext.Devices
            .Where(x => x.Id == deviceId)
            .Select(x => x.ToDomain())
            .FirstOrDefaultAsync(cancellationToken);
    }
    public async Task<bool> ExistAsync(string serialNumber, CancellationToken cancellationToken)
    {
        return await _dbContext.Devices.AnyAsync(x => x.SerialNumber == serialNumber, cancellationToken);
    }
    public async Task<bool> UpdateAsync(Device device, CancellationToken cancellationToken)
    {
        return await _dbContext.Devices
            .Where(x => x.Id == device.Id)
            .ExecuteUpdateAsync(d => d
                .SetProperty(e => e.SerialNumber, device.SerialNumber)
                .SetProperty(e => e.DeviceType, device.DeviceType)
                .SetProperty(e => e.DeviceState, device.DeviceState),
                cancellationToken) > 0;
    }
}
