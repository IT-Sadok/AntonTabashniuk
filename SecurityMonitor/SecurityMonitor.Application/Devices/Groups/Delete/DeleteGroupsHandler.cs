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
        if (command.GroupIds is null || command.GroupIds.Count == 0)
        {
            return Result<bool>.Failure("Failed to delete groups.");
        }

        return await repository.DeleteAsync(command.DeviceId, command.GroupIds, cancellationToken)
            ? Result<bool>.Success(true) 
            : Result<bool>.Failure("Failed to delete groups.");
    }
}
