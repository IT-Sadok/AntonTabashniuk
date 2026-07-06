
using SecurityMonitor.Application.SecuritySchemes.Create;

namespace SecurityMonitor.Api.Endpoints.Auth;

public static class SecuritySchemeEndpoints
{
    public static IEndpointRouteBuilder MapSecuritySchemeEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup(SecuritySchemeRoutes.Base);

        group.MapPost(SecuritySchemeRoutes.Create, Create);

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
}
