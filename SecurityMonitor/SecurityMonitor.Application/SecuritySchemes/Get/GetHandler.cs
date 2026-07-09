using SecurityMonitor.Application.Common;

namespace SecurityMonitor.Application.SecuritySchemes.Get;

public sealed class GetHandler
{
    private readonly ISecuritySchemeRepository repository;

    public GetHandler(ISecuritySchemeRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Result<GetResponse>> Handle(
        GetQuery command,
        CancellationToken cancellationToken)
    {
        if (!await repository.ExistsAsync(command.Id, cancellationToken))
        {
            return Result<GetResponse>.Failure("Security scheme does not exists.");
        }

        var securityScheme = await repository.GetAsync(command.Id, cancellationToken);

        if (securityScheme is not null)
        {
            return Result<GetResponse>.Success(
                new GetResponse(
                    securityScheme.Id, 
                    securityScheme.Name, 
                    securityScheme.Description
                    ));
        }

        return Result<GetResponse>.Failure("Security scheme does not exists.");
    }
}

