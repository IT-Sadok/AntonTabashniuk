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
        var zones = await repository.GetAllAsync(
            command.ZonesIds,
            cancellationToken);

        var existingZoneIds = zones
            .Select(z => z.Id)
            .ToHashSet();

        var zonesToDelete = command.ZonesIds
            .Where(id => existingZoneIds.Contains(id))
            .ToList();

        if (zonesToDelete.Count > 0)
        {
            if(await repository.DeleteAsync(zonesToDelete, cancellationToken))
            {
                return Result<bool>.Success(true);
            }
        }

        return Result<bool>.Failure("Failed to update zones.");
    }
}
