using SecurityMonitor.Domain.Devices;

namespace SecurityMonitor.Application.Devices;

public interface IDeviceRepository
{
    Task<bool> UpdateAsync(Device Device, CancellationToken cancellationToken);
    Task<bool> ExistAsync(string serialNumber, CancellationToken cancellationToken);
    Task<Device?> GetAsync(int securitySchemeId, CancellationToken cancellationToken);
}
