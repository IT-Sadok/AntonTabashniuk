using Microsoft.EntityFrameworkCore;
using SecurityMonitor.Application.Devices.Groups.Zones;
using SecurityMonitor.Domain.Devices.Groups.Zones;
using SecurityMonitor.Infrastructure.Persistence.Mappers;
using SecurityMonitor.Infrastructure.Persistence.Repositories.Helpers;

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
        if(zones is null || zones.Count == 0)
        {
            return false;
        }

        var ids = zones.Select(x => x.Id);

        var zoneEntities = await _dbContext.Zones
            .Where(x => ids.Contains(x.Id))
            .ToListAsync(cancellationToken);
        
        if(UpdateZonesHelper.UpdateZones(zones, zoneEntities))
        {
            return true;
        }

        return false;
    }

    public async Task<List<Zone>> AddRangeAsync(List<Zone> zones, CancellationToken cancellationToken)
    {
        var entities = zones.ToEntity();

        await _dbContext.Zones.AddRangeAsync(entities, cancellationToken);
        await SaveChangesAsync(cancellationToken);

        return entities.Select(e => e.ToDomain()).ToList();
    }

    public async Task<bool> DeleteAsync(List<int> zonesIds, CancellationToken cancellationToken)
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
}
