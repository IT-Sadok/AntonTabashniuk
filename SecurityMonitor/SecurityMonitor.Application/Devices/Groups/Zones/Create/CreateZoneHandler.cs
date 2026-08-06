using SecurityMonitor.Application.Common;
using SecurityMonitor.Application.Devices.Groups.Zones.Mappers;

namespace SecurityMonitor.Application.Devices.Groups.Zones.Create;

public sealed class CreateZoneHandler
{
    private readonly IZoneRepository repository;
    public CreateZoneHandler(IZoneRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Result<CreateZonesResponce>> Handle(
        CreateZoneCommand command,
        CancellationToken cancellationToken)
    {
        if (command.Zones is not null && command.Zones.Count > 0)
        {
            var createdZones = await repository.AddRangeAsync(command.ToDomain(), cancellationToken);
            
            if(createdZones is not null && createdZones.Count > 0) 
            {
                return Result<CreateZonesResponce>.Success(createdZones.ToCreateResponce(command.DeviceId));
            }
        }

        return Result<CreateZonesResponce>.Failure("Failed to update zones.");
    }
}

