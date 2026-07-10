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

        List<SecuritySchemeGetResponse> responceList = new List<SecuritySchemeGetResponse>();

        if (securitySchemes.Count == 0) 
        {
            return Result<SecuritySchemeGetAllResponse>.Failure("Security schemes does not exists.");
        }

        foreach (var securityScheme in securitySchemes)
        {
            if (securityScheme != null)
            {
                responceList.Add(
                    new SecuritySchemeGetResponse(
                     securityScheme.Id,
                     securityScheme.Name,
                     securityScheme.Description
                     ));
            }
        }

        return Result<SecuritySchemeGetAllResponse>.Success(new SecuritySchemeGetAllResponse(responceList));
    }
}

