using SecurityMonitor.Application.Common;

namespace SecurityMonitor.Application.Devices.Groups.Delete;

public class DeleteGroupsHandler : IRequestHandler<DeleteGroupsCommand, Result<bool>>
{
    private readonly IGroupRepository repository;
    public DeleteGroupsHandler(IGroupRepository groupRepository)
    {
        this.repository = groupRepository;
    }
    public async Task<Result<bool>> Handle(
        DeleteGroupsCommand command,
        CancellationToken cancellationToken)
    {
        if (command.GroupIds is not null && command.GroupIds.Count > 0)
        {
            if (await repository.DeleteAsync(command.DeviceId, command.GroupIds, cancellationToken))
            {
                return Result<bool>.Success(true);
            }
        }

        return Result<bool>.Failure("Failed to delete groups.");
    }
}
