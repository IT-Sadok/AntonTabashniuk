
using SecurityMonitor.Application.SecuritySchemes.Create;
using SecurityMonitor.Application.SecuritySchemes.Update;
using SecurityMonitor.Application.SecuritySchemes.Get;
using SecurityMonitor.Application.SecuritySchemes.GetAll;
using SecurityMonitor.Application.SecuritySchemes.Delete;

namespace SecurityMonitor.Api.Endpoints.SecuritySchemes;

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
    private static async Task<IResult> Create(CreateCommand command, CreateHandler handler, CancellationToken cancellationToken)
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

    private static async Task<IResult> Update(UpdateCommand command, UpdateHandler handler, CancellationToken cancellationToken)
    {
        var result = await handler.Handle(command, cancellationToken);

        if (!result.IsSuccess)
        {
            return Results.BadRequest(result.Error);
        }

        return Results.Ok(result.Value);
    }

    private static async Task<IResult> Get(GetQuery command, GetHandler handler, CancellationToken cancellationToken)
    {
        var result = await handler.Handle(command, cancellationToken);

        if (!result.IsSuccess)
        {
            return Results.BadRequest(result.Error);
        }

        return Results.Ok(result.Value);
    }

    private static async Task<IResult> GetAll(GetAllQuery command, GetAllHandler handler, CancellationToken cancellationToken)
    {
        var result = await handler.Handle(command, cancellationToken);

        if (!result.IsSuccess)
        {
            return Results.BadRequest(result.Error);
        }

        return Results.Ok(result.Value);
    }

    private static async Task<IResult> Delete(DeleteCommand command, DeleteHandler handler, CancellationToken cancellationToken)
    {
        var result = await handler.Handle(command, cancellationToken);

        if (!result.IsSuccess)
        {
            return Results.BadRequest(result.Error);
        }

        return Results.Ok();
    }
}
