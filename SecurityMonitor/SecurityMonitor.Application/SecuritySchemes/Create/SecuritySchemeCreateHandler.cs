using SecurityMonitor.Application.Common;
using SecurityMonitor.Domain.Administrative;

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
        if (await repository.ExistsAsync(command.id, cancellationToken))
        {
            return Result<SecuritySchemeCreateResponse>.Failure("Security scheme already exists.");
        }

        var securityScheme = new SecurityScheme(command.id, command.Name, command.Description);

        var id = await repository.AddAsync(securityScheme, cancellationToken);

        if (id > 0)
        {
            return Result<SecuritySchemeCreateResponse>.Success(new SecuritySchemeCreateResponse(id));
        }

        return Result<SecuritySchemeCreateResponse>.Failure("Failed to create security scheme.");
    }
}
