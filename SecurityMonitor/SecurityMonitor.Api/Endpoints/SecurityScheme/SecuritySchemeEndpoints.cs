
using SecurityMonitor.Application.SecuritySchemes.Create;
using SecurityMonitor.Application.SecuritySchemes.Update;
using SecurityMonitor.Application.SecuritySchemes.Get;
using SecurityMonitor.Application.SecuritySchemes.GetAll;
using SecurityMonitor.Application.SecuritySchemes.Delete;

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
    private static async Task<IResult> Create(SecuritySchemeCreateCommand command, SecuritySchemeCreateHandler handler, CancellationToken cancellationToken)
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

    private static async Task<IResult> Update(SecuritySchemeUpdateCommand command, SecuritySchemeUpdateHandler handler, CancellationToken cancellationToken)
    {
        var result = await handler.Handle(command, cancellationToken);

        if (!result.IsSuccess)
        {
            return Results.BadRequest(result.Error);
        }

        return Results.Ok(result.Value);
    }

    private static async Task<IResult> Get(SecuritySchemeGetQuery command, SecuritySchemeGetHandler handler, CancellationToken cancellationToken)
    {
        var result = await handler.Handle(command, cancellationToken);

        if (!result.IsSuccess)
        {
            return Results.BadRequest(result.Error);
        }

        return Results.Ok(result.Value);
    }

    private static async Task<IResult> GetAll(SecuritySchemeGetAllQuery command, SecuritySchemeGetAllHandler handler, CancellationToken cancellationToken)
    {
        var result = await handler.Handle(command, cancellationToken);

        if (!result.IsSuccess)
        {
            return Results.BadRequest(result.Error);
        }

        return Results.Ok(result.Value);
    }

    private static async Task<IResult> Delete(SecuritySchemeDeleteCommand command, SecuritySchemeDeleteHandler handler, CancellationToken cancellationToken)
    {
        var result = await handler.Handle(command, cancellationToken);

        if (!result.IsSuccess)
        {
            return Results.BadRequest(result.Error);
        }

        return Results.Ok();
    }
}
