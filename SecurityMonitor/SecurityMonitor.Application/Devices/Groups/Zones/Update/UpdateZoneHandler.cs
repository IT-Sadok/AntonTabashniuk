using SecurityMonitor.Application.Common;
using SecurityMonitor.Application.Devices.Groups.Zones.Mappers;

namespace SecurityMonitor.Application.Devices.Groups.Zones.Update;

public sealed class UpdateZoneHandler : IRequestHandler<UpdateZoneCommand, Result<bool>>
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
        if (await repository.UpdateAsync(command.ToDomain(), cancellationToken))
        {
            await repository.SaveChangesAsync(cancellationToken);
            return Result<bool>.Success(true);
        }
        return Result<bool>.Failure("Failed to update zones.");
    }
}
