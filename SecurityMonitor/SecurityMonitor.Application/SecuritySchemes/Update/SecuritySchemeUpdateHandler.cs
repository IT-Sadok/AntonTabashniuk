using SecurityMonitor.Application.Common;
using SecurityMonitor.Application.Devices;
using SecurityMonitor.Application.SecuritySchemes.Extensions;
using SecurityMonitor.Application.SecuritySchemes.Mappers;
namespace SecurityMonitor.Application.SecuritySchemes.Update;

public sealed class SecuritySchemeUpdateHandler : IRequestHandler<SecuritySchemeUpdateCommand, Result<SecuritySchemeUpdateResponse>>
{
    private readonly ISecuritySchemeRepository repository;
    private readonly IDeviceRepository deviceRepository;
    public SecuritySchemeUpdateHandler(
        ISecuritySchemeRepository repository,
        IDeviceRepository deviceRepository)
    {
        this.repository = repository;
        this.deviceRepository = deviceRepository;
    }

    public async Task<Result<SecuritySchemeUpdateResponse>> Handle(
        SecuritySchemeUpdateCommand command,
        CancellationToken cancellationToken)
    {
        var securityScheme = await repository.GetAsync(command.Id, cancellationToken);

        if (securityScheme is null)
        {
            return Result<SecuritySchemeUpdateResponse>.Failure("Failed to update security scheme.");
        }

        if (await deviceRepository.ExistAsync(command.Device.SerialNumber, cancellationToken))
        {
            if (!(command.Device.SerialNumber == securityScheme.Device.SerialNumber))
            {
                return Result<SecuritySchemeUpdateResponse>.Failure(
                    $"Can not update security scheme because device with serial number {command.Device.SerialNumber} already exist.");
            }
        }

        SecuritySchemeUpdatersExtensions.InitializeScheme(securityScheme, command.ToSecurityScheme());

        if (await repository.UpdateAsync(securityScheme, cancellationToken))
        {
            await repository.SaveChangesAsync(cancellationToken);
            return Result<SecuritySchemeUpdateResponse>.Success(securityScheme.ToUpdateResponse());
        }
        return Result<SecuritySchemeUpdateResponse>.Failure("Failed to update security scheme.");
    }
}
