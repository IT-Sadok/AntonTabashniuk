using SecurityMonitor.Application.Common;
using SecurityMonitor.Application.Devices.Groups.Mappers;

namespace SecurityMonitor.Application.Devices.Groups.Update;

public class UpdateGroupsHandler : IRequestHandler<UpdateGroupsCommand, Result<UpdateGroupsResponse>>
{
    private readonly IGroupRepository repository;
    public UpdateGroupsHandler(IGroupRepository groupRepository)
    {
        this.repository = groupRepository;
    }

    public async Task<Result<UpdateGroupsResponse>> Handle(
        UpdateGroupsCommand command,
        CancellationToken cancellationToken)
    {
        if (command.Groups is not null && command.Groups.Count > 0)
        {
            var updatedGroups = await repository.UpdateAsync(command.DeviceId, command.ToDomain(), cancellationToken);

            if (updatedGroups is not null && updatedGroups.Count > 0)
            {
                return Result<UpdateGroupsResponse>.Success(updatedGroups.ToUpdateResponce(command.DeviceId));
            }
        }

        return Result<UpdateGroupsResponse>.Failure("Failed to create groups.");
    }
}
