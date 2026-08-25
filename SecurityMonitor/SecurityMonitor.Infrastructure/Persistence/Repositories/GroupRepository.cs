using Microsoft.EntityFrameworkCore;
using SecurityMonitor.Application.Devices.Groups;
using SecurityMonitor.Domain.Devices.Groups.Zones;
using SecurityMonitor.Infrastructure.Persistence.Entities;
using SecurityMonitor.Infrastructure.Persistence.Mappers;
using Group = SecurityMonitor.Domain.Devices.Groups.Group;

namespace SecurityMonitor.Infrastructure.Persistence.Repositories;

public class GroupRepository : IGroupRepository
{
    private readonly ApplicationDbContext _dbContext;

    public GroupRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Group>> UpdateAsync(
        int deviceId,
        List<Group> groups,
        CancellationToken cancellationToken)
    {
        var groupEntities = await _dbContext.Groups
            .Where(x => x.DeviceId == deviceId)
            .ToListAsync(cancellationToken);

        var zoneEntities = await _dbContext.Zones
            .Where(x => x.DeviceId == deviceId)
            .ToListAsync(cancellationToken);

        UpdateGroups(groupEntities, groups, zoneEntities);

        await SaveChangesAsync(cancellationToken);

        return [.. groupEntities
            .Where(x => groups.Any(g => g.Id == x.Id))
            .Select(x => x.ToDomain())];
    }

    public async Task<List<Group>> AddRangeAsync(List<Group> groups, CancellationToken cancellationToken)
    {
        var entities = groups.ToEntity();

        await _dbContext.Groups.AddRangeAsync(entities, cancellationToken);
        await SaveChangesAsync(cancellationToken);

        return [.. entities.Select(e => e.ToDomain())];
    }

    public async Task<bool> DeleteAsync(IReadOnlyList<int> groupsIds, CancellationToken cancellationToken)
    {
        return await _dbContext.Groups
            .Where(x => groupsIds.Contains(x.Id))
            .ExecuteDeleteAsync(cancellationToken) > 0;
    }

    public async Task<List<Group>> GetAllAsync(IReadOnlyList<int> groups, CancellationToken cancellationToken)
    {
        return await _dbContext.Groups
             .Where(x => groups.Contains(x.Id))
             .Select(x => x.ToDomain())
             .ToListAsync(cancellationToken);
    }
    
    public async Task<List<string>> GetExistingGroupNamesAsync(int deviceId, IReadOnlyCollection<string> groupNames, CancellationToken cancellationToken)
    {
        return await _dbContext.Groups
            .Where(x => x.DeviceId == deviceId && groupNames.Contains(x.Name))
            .Select(x => x.Name)
            .ToListAsync(cancellationToken);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }

    #region Helpers
    private void UpdateGroups(
        List<GroupEntity> groupEntities,
        List<Group> groups,
        List<ZoneEntity> zoneEntities)
    {
        var groupsById = groupEntities.ToDictionary(x => x.Id);
        var zonesById = zoneEntities.ToDictionary(x => x.Id);

        // DELETE GROUPS
        var requestGroupIds = groups
            .Where(x => x.Id != 0)
            .Select(x => x.Id)
            .ToHashSet();

        var groupsToDelete = groupEntities
            .Where(x => !requestGroupIds.Contains(x.Id))
            .ToList();

        _dbContext.Groups.RemoveRange(groupsToDelete);

        // UPDATE GROUPS
        foreach (var group in groups.Where(x => x.Id != 0))
        {
            if (!groupsById.TryGetValue(
                    group.Id,
                    out var entity))
            {
                continue;
            }

            entity.Name = group.Name;
            entity.State = group.State;

            UpdateGroupZones(entity.Id, group.Zones, zonesById);
        }

        // CREATE GROUPS
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
    }

    private static void UpdateGroupZones(
        int groupId,
        List<Zone> requestedZones,
        Dictionary<int, ZoneEntity> zonesById)
    {
        var requestedZoneIds = requestedZones
            .Select(x => x.Id)
            .ToHashSet();

        // DELETE RELATIONSHIP
        foreach (var zoneEntity in zonesById.Values)
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
            if (!zonesById.TryGetValue(
                    zoneId,
                    out var zoneEntity))
            {
                continue;
            }

            zoneEntity.GroupId = groupId;
        }
    }

    #endregion

}
