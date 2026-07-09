using SecurityMonitor.Application.Common;
using SecurityMonitor.Application.SecuritySchemes.Get;
using SecurityMonitor.Domain.Administrative;

namespace SecurityMonitor.Application.SecuritySchemes.GetAll;


public sealed class GetAllHandler
{
    private readonly ISecuritySchemeRepository repository;

    public GetAllHandler(ISecuritySchemeRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Result<GetAllResponse>> Handle(
        GetAllQuery command,
        CancellationToken cancellationToken)
    {
        List<SecurityScheme> securitySchemes = await repository.GetAllAsync(cancellationToken);

        List<GetResponse> responceList = new List<GetResponse>();

        if (securitySchemes.Count == 0) 
        {
            return Result<GetAllResponse>.Failure("Security schemes does not exists.");
        }

        foreach (var securityScheme in securitySchemes)
        {
            if (securityScheme != null)
            {
                responceList.Add(
                    new GetResponse(
                     securityScheme.Id,
                     securityScheme.Name,
                     securityScheme.Description
                     ));
            }
        }

        return Result<GetAllResponse>.Success(new GetAllResponse(responceList));
    }
}

