using SecurityMonitor.Application.Common;
using SecurityMonitor.Application.Devices.Groups.Mappers;

namespace SecurityMonitor.Application.Devices.Groups.Create;

public class CreateGroupsHandler : IRequestHandler<CreateGroupsCommand, Result<CreateGroupsResponse>>
{
    private readonly IGroupRepository repository;
    public CreateGroupsHandler(IGroupRepository groupRepository) 
    {
        this.repository = groupRepository;
    }
    public async Task<Result<CreateGroupsResponse>> Handle(
        CreateGroupsCommand command, 
        CancellationToken cancellationToken)
    {
        if (command.Groups is null || command.Groups.Count == 0)
        {
            return Result<CreateGroupsResponse>.Failure("Failed to create groups."); 
        }

        var existingGroups = await repository.GetExistingGroupNamesAsync(
            command.DeviceId,
            [.. command.Groups.Select(x=> x.Name)], 
            cancellationToken);

        if(existingGroups is null || existingGroups.Count == 0) 
        {
            var createGroupsIds = await repository.AddRangeAsync(command.DeviceId, command.ToDomain(), cancellationToken);

            if (createGroupsIds is not null && createGroupsIds.Count > 0)
            {
                return Result<CreateGroupsResponse>.Success(createGroupsIds.ToCreateResponce(command.DeviceId));
            }
        }

        return Result<CreateGroupsResponse>.Failure("Failed to create groups.");
    }
}
