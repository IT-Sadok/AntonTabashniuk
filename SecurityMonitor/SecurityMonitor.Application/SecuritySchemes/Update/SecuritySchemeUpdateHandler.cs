using SecurityMonitor.Application.Common;
using SecurityMonitor.Domain.Administrative;

namespace SecurityMonitor.Application.SecuritySchemes.Update;

public sealed class SecuritySchemeUpdateHandler
{
    private readonly ISecuritySchemeRepository repository;

    public SecuritySchemeUpdateHandler(ISecuritySchemeRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Result<SecuritySchemeUpdateResponse>> Handle(
        SecuritySchemeUpdateCommand command,
        CancellationToken cancellationToken)
    {
        if (!await repository.ExistsAsync(command.Id, cancellationToken))
        {
            return Result<SecuritySchemeUpdateResponse>.Failure("Security scheme does not exists.");
        }
        var securityScheme = new SecurityScheme(command.Id, command.Name, command.Description);

        var isUpdated = await repository.UpdateAsync(securityScheme, cancellationToken);

        if (isUpdated)
        {
            return Result<SecuritySchemeUpdateResponse>.Success(
                new SecuritySchemeUpdateResponse(
                    securityScheme.Id,
                    securityScheme.Name,
                    securityScheme.Description
                    ));
        }

        return Result<SecuritySchemeUpdateResponse>.Failure("Failed to update security scheme.");
    }
}
