using SecurityMonitor.Application.Common;
using SecurityMonitor.Application.SecuritySchemes.Mappers;

namespace SecurityMonitor.Application.SecuritySchemes.Create;

public sealed class SecuritySchemeCreateHandler
{
    private readonly ISecuritySchemeRepository repository;

    public SecuritySchemeCreateHandler(
        ISecuritySchemeRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Result<SecuritySchemeCreateResponse>> Handle(
        SecuritySchemeCreateCommand command,
        CancellationToken cancellationToken)
    {
        if (await repository.ExistsAsync(command.Name, cancellationToken))
        {
            return Result<SecuritySchemeCreateResponse>.Failure("Security scheme already exists.");
        }

        var entityId = await repository.AddAsync(command.ToSecurityScheme(), cancellationToken);

        if (entityId > 0)
        {
            return Result<SecuritySchemeCreateResponse>.Success(new SecuritySchemeCreateResponse(entityId));
        }

        return Result<SecuritySchemeCreateResponse>.Failure("Failed to create security scheme.");
    }
}
