namespace SecurityMonitor.Application.SecuritySchemes.GetAll;

public sealed record SecuritySchemeGetAllQuery(
    int Page = 1,
    int PageSize = 20
    );
