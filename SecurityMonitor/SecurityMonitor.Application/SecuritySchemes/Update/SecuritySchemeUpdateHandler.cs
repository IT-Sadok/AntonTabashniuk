using SecurityMonitor.Application.Common;
using SecurityMonitor.Application.SecuritySchemes.Mappers;

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
        var securityScheme = await repository.GetAsync(command.Id, cancellationToken);
                        
        if (securityScheme is not null)
        {
            securityScheme.Update(command.Name, command.Description);
            
            if (await repository.UpdateAsync(securityScheme, cancellationToken))
            {
                return Result<SecuritySchemeUpdateResponse>.Success(securityScheme.ToUpdateResponse());
            }
        }

        return Result<SecuritySchemeUpdateResponse>.Failure("Failed to update security scheme.");
    }
}
