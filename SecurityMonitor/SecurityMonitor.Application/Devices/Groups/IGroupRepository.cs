using SecurityMonitor.Domain.Devices.Groups;

namespace SecurityMonitor.Application.Devices.Groups;

public interface IGroupRepository
{
    Task<List<Group>> AddRangeAsync(int deviceId, List<Group> groups, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int deviceId, IReadOnlyList<int> groupsIds, CancellationToken cancellationToken);
    Task<List<Group>> UpdateAsync(int deviceId, List<Group> groups, CancellationToken cancellationToken);
    Task<List<string>> GetExistingGroupNamesAsync(int deviceId, IReadOnlyCollection<string> groupNames, CancellationToken cancellationToken);
    Task<List<Group>> GetAllAsync(IReadOnlyList<int> groups, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
