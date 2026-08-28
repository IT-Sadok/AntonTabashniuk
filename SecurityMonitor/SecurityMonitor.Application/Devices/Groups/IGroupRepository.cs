using SecurityMonitor.Domain.Devices.Groups;

namespace SecurityMonitor.Application.Devices.Groups;

public interface IGroupRepository
{
    Task<IReadOnlyList<int>> AddRangeAsync(int deviceId, List<Group> groups, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int deviceId, IReadOnlyList<int> groups, CancellationToken cancellationToken);
    Task<IReadOnlyList<Group>> UpdateAsync(int deviceId, List<Group> groups, CancellationToken cancellationToken);
    Task<IReadOnlyList<string>> GetExistingGroupNamesAsync(int deviceId, IReadOnlyCollection<string> groups, CancellationToken cancellationToken);
    Task<IReadOnlyList<Group>> GetAllAsync(IReadOnlyList<int> groups, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
