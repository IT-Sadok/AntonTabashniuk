using SecurityMonitor.Application.Common;

namespace SecurityMonitor.Application.SecuritySchemes.Get;

public sealed class SecuritySchemeGetHandler
{
    private readonly ISecuritySchemeRepository repository;

    public SecuritySchemeGetHandler(ISecuritySchemeRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Result<SecuritySchemeGetResponse>> Handle(
        SecuritySchemeGetQuery command,
        CancellationToken cancellationToken)
    {
        if (!await repository.ExistsAsync(command.Id, cancellationToken))
        {
            return Result<SecuritySchemeGetResponse>.Failure("Security scheme does not exists.");
        }

        var securityScheme = await repository.GetAsync(command.Id, cancellationToken);

        if (securityScheme is not null)
        {
            return Result<SecuritySchemeGetResponse>.Success(
                new SecuritySchemeGetResponse(
                    securityScheme.Id, 
                    securityScheme.Name, 
                    securityScheme.Description
                    ));
        }

        return Result<SecuritySchemeGetResponse>.Failure("Security scheme does not exists.");
    }
}

