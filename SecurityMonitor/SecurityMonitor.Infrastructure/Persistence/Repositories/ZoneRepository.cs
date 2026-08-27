using Microsoft.EntityFrameworkCore;
using SecurityMonitor.Application.Devices.Groups.Zones;
using SecurityMonitor.Domain.Devices.Groups.Zones;
using SecurityMonitor.Infrastructure.Persistence.Entities;
using SecurityMonitor.Infrastructure.Persistence.Mappers;

namespace SecurityMonitor.Infrastructure.Persistence.Repositories;

public class ZoneRepository : IZoneRepository
{

    private readonly ApplicationDbContext _dbContext;
    public ZoneRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<bool> UpdateAsync(List<Zone> zones, CancellationToken cancellationToken)
    {
        var ids = zones.Select(x => x.Id);

        var zoneEntities = await _dbContext.Zones
            .Where(x => ids.Contains(x.Id))
            .ToListAsync(cancellationToken);
        
        if(zoneEntities is null || zoneEntities.Count == 0)
        {
            return false;
        }

        return UpdateZones(zoneEntities, zones);
    }

    public async Task<List<Zone>> AddRangeAsync(List<Zone> zones, CancellationToken cancellationToken)
    {
        var entities = zones.ToEntity();

        await _dbContext.Zones.AddRangeAsync(entities, cancellationToken);
        await SaveChangesAsync(cancellationToken);

        return entities.Select(e => e.ToDomain()).ToList();
    }

    public async Task<bool> DeleteAsync(IReadOnlyList<int> zonesIds, CancellationToken cancellationToken)
    {
        return await _dbContext.Zones
            .Where(x => zonesIds.Contains(x.Id))
            .ExecuteDeleteAsync(cancellationToken) > 0;
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken)
    {
        return await _dbContext.Zones.AnyAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<List<Zone>> GetAllAsync(IReadOnlyList<int> zones, CancellationToken cancellationToken)
    {
        return await _dbContext.Zones
            .Where(x => zones.Contains(x.Id))
            .Select(x => x.ToDomain())
            .ToListAsync(cancellationToken);
    }

    public async Task<Zone?> GetAsync(int id, CancellationToken cancellationToken)
    {
        var zoneEntity = await _dbContext.Zones.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return zoneEntity?.ToDomain();
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }

    #region Helpers
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
            entity.GroupId = entity.GroupId;
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
