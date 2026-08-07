using SecurityMonitor.Application.Common;

namespace SecurityMonitor.Application.Devices.Groups.Zones.Delete;

public sealed class DeleteZoneHandler 
{
    private readonly IZoneRepository repository;
    public DeleteZoneHandler(IZoneRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Result<bool>> Handle(
        DeleteZoneCommand command,
        CancellationToken cancellationToken)
    {
        if (command.ZonesIds is not null && command.ZonesIds.Count > 0)
        {
            if(await repository.DeleteAsync(command.ZonesIds, cancellationToken))
            {
                return Result<bool>.Success(true);
            }
        }

        return Result<bool>.Failure("Failed to update zones.");
    }
}
