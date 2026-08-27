using Microsoft.EntityFrameworkCore;
using SecurityMonitor.Application.SecuritySchemes;
using SecurityMonitor.Domain.Administrative;
using SecurityMonitor.Domain.Devices;
using SecurityMonitor.Domain.Devices.Groups;
using SecurityMonitor.Domain.Devices.Groups.Zones;
using SecurityMonitor.Infrastructure.Persistence.Entities;
using SecurityMonitor.Infrastructure.Persistence.Mappers;

namespace SecurityMonitor.Infrastructure.Persistence.Repositories;

public class SecuritySchemeRepository : ISecuritySchemeRepository
{
    private readonly ApplicationDbContext _dbContext;
    public SecuritySchemeRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<int> GetTotalCountAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.SecuritySchemes.CountAsync(cancellationToken);
    }
    public async Task<List<SecurityScheme>> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken)
    {
        return await _dbContext.SecuritySchemes
            .Include(x => x.Device)
            .OrderBy(x => x.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(x => x.ToDomain())
            .ToListAsync(cancellationToken);
    }

    public async Task<SecurityScheme?> GetAsync(int securitySchemeId, CancellationToken cancellationToken)
    {
        return await _dbContext.SecuritySchemes
            .Include(p => p.Device)
            .Where(x => x.Id == securitySchemeId)
            .Select(x => x.ToDomain())
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> DeleteAsync(int securitySchemeId, CancellationToken cancellationToken)
    {
        return await _dbContext.SecuritySchemes
            .Where(x => x.Id == securitySchemeId)
            .ExecuteDeleteAsync(cancellationToken) > 0;
    }

    public async Task<bool> UpdateAsync(SecurityScheme securityScheme, CancellationToken cancellationToken)
    {
        var securitySchemeEntity = await _dbContext.SecuritySchemes
            .Include(p => p.Device)
                .ThenInclude(x => x.Zones)
            .FirstOrDefaultAsync(x => x.Id == securityScheme.Id, cancellationToken);

        if (securitySchemeEntity is null)
        {
            return false;
        }
        securitySchemeEntity.Name = securityScheme.Name;
        securitySchemeEntity.Description = securityScheme.Description;
        return UpdateDevice(securitySchemeEntity.Device, securityScheme.Device);
    }

    public async Task<int> AddAsync(SecurityScheme securityScheme, CancellationToken ct)
    {
        var entity = securityScheme.ToEntity();

        await _dbContext.SecuritySchemes.AddAsync(entity, ct);
        await SaveChangesAsync(ct);

        return entity.Id;
    }
    public async Task<bool> ExistsAsync(string name, CancellationToken ct)
    {
        return await _dbContext.SecuritySchemes.AnyAsync(x => x.Name == name, ct);
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken ct)
    {
        return await _dbContext.SecuritySchemes.AnyAsync(x => x.Id == id, ct);
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

        if (!UpdateZones(deviceEntity.Zones, device.Zones))
        {
            return false;
        }

        if (!UpdateGroups(deviceEntity.Groups, device.Groups, deviceEntity.Zones))
        {
            return false;
        }
        
        return true;
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
    private bool UpdateGroups(
        List<GroupEntity> groupEntities,
        List<Group> groups,
        List<ZoneEntity> zoneEntities)
    {
        groupEntities ??= [];
        groups ??= [];
        zoneEntities ??= [];

        var dbDictionary = groupEntities.ToDictionary(x => x.Id);
        var zonesDictionary = zoneEntities.ToDictionary(x => x.Id);

        // DELETE
        var requestIds = groups
            .Where(x => x.Id != 0)
            .Select(x => x.Id)
            .ToHashSet();

        var entitiesToDelete = groupEntities
            .Where(x => !requestIds.Contains(x.Id))
            .ToList();

        _dbContext.Groups.RemoveRange(entitiesToDelete);

        // UPDATE
        foreach (var group in groups.Where(x => x.Id != 0))
        {
            if (!dbDictionary.TryGetValue(
                    group.Id,
                    out var entity))
            {
                return false;
            }

            entity.Name = group.Name;
            entity.State = group.State;

            UpdateGroupZones(group.Id, group.Zones, zonesDictionary);
        }

        // CREATE
        foreach (var group in groups.Where(x => x.Id == 0))
        {
            var entity = new GroupEntity
            {
                DeviceId = group.DeviceId,
                Name = group.Name,
                State = group.State
            };

            _dbContext.Groups.Add(entity);
        }

        return true;
    }

    private static void UpdateGroupZones(
        int groupId,
        List<Zone> requestedZones,
        Dictionary<int, ZoneEntity> zonesDictionary)
    {
        var requestedZoneIds = requestedZones
            .Select(x => x.Id)
            .ToHashSet();

        // DELETE RELATIONSHIP
        foreach (var zoneEntity in zonesDictionary.Values)
        {
            if (zoneEntity.GroupId == groupId &&
                !requestedZoneIds.Contains(zoneEntity.Id))
            {
                zoneEntity.GroupId = null;
            }
        }

        // CREATE / UPDATE RELATIONSHIP
        foreach (var zoneId in requestedZoneIds)
        {
            if (!zonesDictionary.TryGetValue(
                    zoneId,
                    out var zoneEntity))
            {
                return;
            }

            zoneEntity.GroupId = groupId;
        }
    }

    #endregion
}
