using SecurityMonitor.Application.Common;
using SecurityMonitor.Application.Devices;
using SecurityMonitor.Application.SecuritySchemes.Mappers;

namespace SecurityMonitor.Application.SecuritySchemes.Create;

public sealed class SecuritySchemeCreateHandler
{
    private readonly ISecuritySchemeRepository repository;
    private readonly IDeviceRepository deviceRepository;

    public SecuritySchemeCreateHandler(
        ISecuritySchemeRepository repository,
        IDeviceRepository deviceRepository
        )
    {
        this.repository = repository;
        this.deviceRepository = deviceRepository;
    }

    public async Task<Result<SecuritySchemeCreateResponse>> Handle(
        SecuritySchemeCreateCommand command,
        CancellationToken cancellationToken)
    {
        if (await repository.ExistsAsync(command.Name, cancellationToken))
        {
            return Result<SecuritySchemeCreateResponse>.Failure("Security scheme already exists.");
        }

        if (await deviceRepository.ExistAsync(command.Device.SerialNumber, cancellationToken))
        {
            return Result<SecuritySchemeCreateResponse>.Failure(
                $"Can not save security scheme because device with serial number {command.Device.SerialNumber} already exist.");
        }

        var entityId = await repository.AddAsync(command.ToSecurityScheme(), cancellationToken);

        if (entityId > 0)
        {
            return Result<SecuritySchemeCreateResponse>.Success(new SecuritySchemeCreateResponse(entityId));
        }

        return Result<SecuritySchemeCreateResponse>.Failure("Failed to create security scheme.");
    }
}
