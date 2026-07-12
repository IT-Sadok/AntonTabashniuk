using SecurityMonitor.Application.Common;
using SecurityMonitor.Application.SecuritySchemes.Mappers;

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
        var securityScheme = await repository.GetAsync(command.Id, cancellationToken);

        if (securityScheme is not null)
        {
            return Result<SecuritySchemeGetResponse>.Success(securityScheme.ToGetResponse());
        }

        return Result<SecuritySchemeGetResponse>.Failure("Security scheme does not exists.");
    }
}

