using SecurityMonitor.Application.Common;
using SecurityMonitor.Application.SecuritySchemes.Get;
using SecurityMonitor.Application.SecuritySchemes.Mappers;
using SecurityMonitor.Domain.Administrative;

namespace SecurityMonitor.Application.SecuritySchemes.GetAll;


public sealed class SecuritySchemeGetAllHandler : IRequestHandler<SecuritySchemeGetAllQuery, Result<PagedResult<SecuritySchemeGetResponse>>>
{
    private readonly ISecuritySchemeRepository repository;
    private const int MaxPageSize = 50;
    public SecuritySchemeGetAllHandler(ISecuritySchemeRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Result<PagedResult<SecuritySchemeGetResponse>>> Handle(
        SecuritySchemeGetAllQuery query, 
        CancellationToken cancellationToken)
    {
        var validation = ValidateQuery(query);

        if (!validation.IsValid)
        {
            return Result<PagedResult<SecuritySchemeGetResponse>>
                .Failure(validation.Error!);
        }

        int totalCount = await repository.GetTotalCountAsync(cancellationToken);

        List<SecurityScheme> securitySchemes = await repository.GetAllAsync(
            query.Page,
            query.PageSize, 
            cancellationToken);

        var response = securitySchemes
            .Select(x => x.ToGetResponse())
            .ToList();

        return Result<PagedResult<SecuritySchemeGetResponse>>.Success(new PagedResult<SecuritySchemeGetResponse>(
            response,
            totalCount,
            query.Page,
            query.PageSize));
    }

    private (bool IsValid, string? Error) ValidateQuery(SecuritySchemeGetAllQuery query)
    {
        if (query.Page <= 0)
        {
            return (false, "Page must be greater than 0.");
        }

        if (query.PageSize <= 0)
        {
            return (false, "PageSize must be greater than 0.");
        }

        if (query.PageSize > MaxPageSize)
        {
            return (false, $"PageSize cannot exceed {MaxPageSize}.");
        }

        return (true, null);
    }
}

