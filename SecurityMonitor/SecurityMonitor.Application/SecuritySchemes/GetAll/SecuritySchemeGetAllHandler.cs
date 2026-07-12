using SecurityMonitor.Application.Common;
using SecurityMonitor.Application.SecuritySchemes.Mappers;
using SecurityMonitor.Domain.Administrative;

namespace SecurityMonitor.Application.SecuritySchemes.GetAll;


public sealed class SecuritySchemeGetAllHandler
{
    private readonly ISecuritySchemeRepository repository;

    public SecuritySchemeGetAllHandler(ISecuritySchemeRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Result<SecuritySchemeGetAllResponse>> Handle(CancellationToken cancellationToken)
    {
        List<SecurityScheme> securitySchemes = await repository.GetAllAsync(cancellationToken);

        var responseList = securitySchemes
            .Select(x => x.ToGetResponse())
            .ToList();

        return Result<SecuritySchemeGetAllResponse>.Success(new SecuritySchemeGetAllResponse(responseList));
    }
}

