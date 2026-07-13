
using SecurityMonitor.Application.SecuritySchemes.Create;
using SecurityMonitor.Application.SecuritySchemes.Update;
using SecurityMonitor.Application.SecuritySchemes.Get;
using SecurityMonitor.Application.SecuritySchemes.GetAll;
using SecurityMonitor.Application.SecuritySchemes.Delete;
using SecurityMonitor.Api.Endpoints.SecurityScheme.Requests;

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

    private static async Task<IResult> Update(int id, SecuritySchemeUpdateRequest request, SecuritySchemeUpdateHandler handler, CancellationToken cancellationToken)
    {
        var command = new SecuritySchemeUpdateCommand(id, request.Name, request.Description);

        var result = await handler.Handle(command, cancellationToken);

        if (!result.IsSuccess)
        {
            return Results.BadRequest(result.Error);
        }

        return Results.Ok(result.Value);
    }

    private static async Task<IResult> Get(int id, SecuritySchemeGetHandler handler, CancellationToken cancellationToken)
    {
        var command = new SecuritySchemeGetQuery(id);

        var result = await handler.Handle(command, cancellationToken);

        if (!result.IsSuccess)
        {
            return Results.BadRequest(result.Error);
        }

        return Results.Ok(result.Value);
    }

    private static async Task<IResult> GetAll(SecuritySchemeGetAllHandler handler, CancellationToken cancellationToken)
    {
        var result = await handler.Handle(cancellationToken);

        if (!result.IsSuccess)
        {
            return Results.BadRequest(result.Error);
        }

        return Results.Ok(result.Value);
    }

    private static async Task<IResult> Delete(int id, SecuritySchemeDeleteHandler handler, CancellationToken cancellationToken)
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
