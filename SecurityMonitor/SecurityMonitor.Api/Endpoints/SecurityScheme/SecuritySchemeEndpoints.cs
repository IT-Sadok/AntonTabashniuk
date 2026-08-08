
using SecurityMonitor.Api.Endpoints.SecurityScheme.Mappers;
using SecurityMonitor.Api.Endpoints.SecurityScheme.Requests;
using SecurityMonitor.Application.Common;
using SecurityMonitor.Application.SecuritySchemes.Create;
using SecurityMonitor.Application.SecuritySchemes.Delete;
using SecurityMonitor.Application.SecuritySchemes.Get;
using SecurityMonitor.Application.SecuritySchemes.GetAll;
using SecurityMonitor.Application.SecuritySchemes.Update;
namespace SecurityMonitor.Api.Endpoints.SecurityScheme;

public static class SecuritySchemeEndpoints
{
    public static IEndpointRouteBuilder MapSecuritySchemeEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup(SecuritySchemeRoutes.Base);

        group.MapPost(SecuritySchemeRoutes.Create, Create);

        group.MapGet(SecuritySchemeRoutes.Get, Get);

        group.MapGet(SecuritySchemeRoutes.GetAll, GetAll);

        group.MapPut(SecuritySchemeRoutes.Update, Update);

        group.MapDelete(SecuritySchemeRoutes.Delete, Delete);

        return endpoints;
    }   
    private static async Task<IResult> Create(
        SecuritySchemeCreateCommand command, 
        IRequestHandler<SecuritySchemeCreateCommand, Result<SecuritySchemeCreateResponse>> handler, 
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(command, cancellationToken);

        if (!result.IsSuccess)
        {
            return Results.BadRequest(result.Error);
        }

        return Results.Created(
            $"{SecuritySchemeRoutes.Base}{SecuritySchemeRoutes.Create}{result.Value}",
            result.Value);
    }

    private static async Task<IResult> Update(
        int id,
        SecuritySchemeUpdateRequest request,
        IRequestHandler<SecuritySchemeUpdateCommand, Result<SecuritySchemeUpdateResponse>> handler, 
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(request.ToCommand(id), cancellationToken);

        if (!result.IsSuccess)
        {
            return Results.BadRequest(result.Error);
        }

        return Results.Ok(result.Value);
    }

    private static async Task<IResult> Get(
        int id,
        IRequestHandler<SecuritySchemeGetQuery, Result<SecuritySchemeGetResponse>> handler, 
        CancellationToken cancellationToken)
    {
        var command = new SecuritySchemeGetQuery(id);

        var result = await handler.Handle(command, cancellationToken);

        if (!result.IsSuccess)
        {
            return Results.BadRequest(result.Error);
        }

        return Results.Ok(result.Value);
    }

    private static async Task<IResult> GetAll(
        [AsParameters] SecuritySchemeGetAllQuery query,
        IRequestHandler<SecuritySchemeGetAllQuery, Result<PagedResult<SecuritySchemeGetResponse>>> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(query, cancellationToken);

        if (!result.IsSuccess)
        {
            return Results.BadRequest(result.Error);
        }

        return Results.Ok(result.Value);
    }

    private static async Task<IResult> Delete(
        int id,
        IRequestHandler<SecuritySchemeDeleteCommand, Result<bool>> handler, 
        CancellationToken cancellationToken)
    {
        var command = new SecuritySchemeDeleteCommand(id);
        var result = await handler.Handle(command, cancellationToken);

        if (!result.IsSuccess)
        {
            return Results.BadRequest(result.Error);
        }

        return Results.Ok();
    }
}
