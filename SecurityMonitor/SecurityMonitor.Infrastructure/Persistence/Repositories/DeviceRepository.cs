using Microsoft.EntityFrameworkCore;
using SecurityMonitor.Application.Devices;
using SecurityMonitor.Domain.Administrative;
using SecurityMonitor.Domain.Devices;
using SecurityMonitor.Infrastructure.Persistence.Mappers;
using SecurityMonitor.Infrastructure.Persistence.Repositories.Helpers;
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
        var deviceEntity = await _dbContext.Devices
            .Include(g => g.Zones)
            .FirstOrDefaultAsync(x => x.Id == device.Id, cancellationToken);

        return UpdateDeviceHelper.UpdateDevice(device, deviceEntity!, _dbContext);
    }

    public Task SaveChangesAsync(CancellationToken ct)
    {
        return _dbContext.SaveChangesAsync(ct);
    }
}
