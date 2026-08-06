using SecurityMonitor.Application.Common;
using SecurityMonitor.Application.Devices.Groups.Zones.Mappers;

namespace SecurityMonitor.Application.Devices.Groups.Zones.Update;

public sealed class UpdateZoneHandler
{
    private readonly IZoneRepository repository;
    public UpdateZoneHandler(IZoneRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Result<bool>> Handle(
        UpdateZoneCommand command,
        CancellationToken cancellationToken)
    {
        var zones = await repository.GetAllAsync(
            command.Zones.Select(z => z.ZoneId).ToList(),
            cancellationToken);

        var existingZoneIds = zones
            .Select(z => z.Id)
            .ToHashSet();

        var zonesToCreate = command.Zones
            .Where(z => !existingZoneIds.Contains(z.ZoneId))
            .Select(z => z.ToDomain(command.DeviceId))
            .ToList();

        var zonesToUpdate = command.Zones
            .Where(z => existingZoneIds.Contains(z.ZoneId))
            .Select(z => z.ToDomain(command.DeviceId))
            .ToList();

        if (zonesToCreate.Count > 0)
        {
            var addedZoneIds = await repository.AddRangeAsync(zonesToCreate, cancellationToken);
            
            if (addedZoneIds is null || addedZoneIds.Count == 0)
            {
                return Result<bool>.Failure("Failed to update zones.");
            }
        }

        if (zonesToUpdate.Count > 0)
        {
            if (await repository.UpdateAsync(zonesToUpdate, cancellationToken))
            {
                await repository.SaveChangesAsync(cancellationToken);
            }
            else
            {   
                return Result<bool>.Failure("Failed to update zones.");
            }
        }

        return Result<bool>.Success(true);
    }
}
