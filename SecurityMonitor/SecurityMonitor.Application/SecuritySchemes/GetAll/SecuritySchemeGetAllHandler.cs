using SecurityMonitor.Application.Common;
using SecurityMonitor.Application.SecuritySchemes.Get;
using SecurityMonitor.Domain.Administrative;

namespace SecurityMonitor.Application.SecuritySchemes.GetAll;


public sealed class SecuritySchemeGetAllHandler
{
    private readonly ISecuritySchemeRepository repository;

    public SecuritySchemeGetAllHandler(ISecuritySchemeRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Result<SecuritySchemeGetAllResponse>> Handle(
        SecuritySchemeGetAllQuery command,
        CancellationToken cancellationToken)
    {
        List<SecurityScheme> securitySchemes = await repository.GetAllAsync(cancellationToken);

        var responseList = securitySchemes
            .Select(x => new SecuritySchemeGetResponse(
                x.Id,
                x.Name,
                x.Description))
            .ToList();

        return Result<SecuritySchemeGetAllResponse>.Success(new SecuritySchemeGetAllResponse(responseList));
    }
}

