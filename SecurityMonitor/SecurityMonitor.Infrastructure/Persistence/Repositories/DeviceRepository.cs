using Microsoft.EntityFrameworkCore;
using SecurityMonitor.Application.Devices;
using SecurityMonitor.Domain.Devices;
using SecurityMonitor.Domain.Devices.Groups.Zones;
using SecurityMonitor.Infrastructure.Persistence.Entities;
using SecurityMonitor.Infrastructure.Persistence.Mappers;

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

        if (deviceEntity is null)
        {
            return false;
        }
        
        return UpdateDevice(deviceEntity, device); 
    }

    public Task SaveChangesAsync(CancellationToken ct)
    {
        return _dbContext.SaveChangesAsync(ct);
    }
    #region Helpers
    private bool UpdateDevice(DeviceEntity deviceEntity, Device device)
    {
        if (deviceEntity is null)
        {
            return false;
        }

        deviceEntity.SecuritySchemeId = device.SecuritySchemeId;
        deviceEntity.SerialNumber = device.SerialNumber;
        deviceEntity.DeviceState = device.DeviceState;
        deviceEntity.DeviceType = device.DeviceType;
        return UpdateZones(deviceEntity.Zones, device.Zones);
    }

    private bool UpdateZones(List<ZoneEntity> zoneEntities, List<Zone> zones)
    {
        zoneEntities ??= [];

        var dbDictionary = zoneEntities.ToDictionary(x => x.Id);

        // DELETE
        var requestIds = zones
            .Where(x => x.Id != 0)
            .Select(x => x.Id)
            .ToHashSet();

        var entitiesToDelete = zoneEntities
            .Where(x => !requestIds.Contains(x.Id))
            .ToList();

        _dbContext.Zones.RemoveRange(entitiesToDelete);

        // UPDATE
        foreach (var zone in zones.Where(x => x.Id != 0))
        {
            if (!dbDictionary.TryGetValue(zone.Id, out var entity))
            {
                return false;
            }

            entity.Name = zone.Name;
            entity.GroupId = null;
            entity.State = zone.State;
            entity.Type = zone.Type;
        }

        // CREATE
        var entitiesToCreate = zones
            .Where(x => x.Id == 0)
            .Select(zone => new ZoneEntity
            {
                DeviceId = zone.DeviceId,
                GroupId = null,
                Name = zone.Name,
                State = zone.State,
                Type = zone.Type
            });

        _dbContext.Zones.AddRange(entitiesToCreate);

        return true;
    }
    #endregion
}
