using SecurityMonitor.Application.Common;
using SecurityMonitor.Application.Devices.Mappers;

namespace SecurityMonitor.Application.Devices.Update;

public class DeviceUpdateHandler : IRequestHandler<DeviceUpdateCommand, Result<bool>>
{
    private readonly IDeviceRepository repository;

    public DeviceUpdateHandler(IDeviceRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Result<bool>> Handle(
        DeviceUpdateCommand command,
        CancellationToken cancellationToken)
    {
        var device = await repository.GetAsync(command.DeviceId, cancellationToken);

        if (device is not null)
        {
            if (await repository.ExistAsync(command.SerialNumber, cancellationToken)) 
            {
                return Result<bool>.Failure($"Device with serial number {command.SerialNumber} already exist");
            }
             
            device.InitializeDevice(command.ToDomain());

            if (await repository.UpdateAsync(device, cancellationToken))
            {
                await repository.SaveChangesAsync(cancellationToken);
                return Result<bool>.Success(true);
            }
        }

        return Result<bool>.Failure("Failed to update device.");
    }
}
