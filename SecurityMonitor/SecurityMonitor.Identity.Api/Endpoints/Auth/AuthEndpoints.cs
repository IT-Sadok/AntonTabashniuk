using SecurityMonitor.Identity.Application.Authentication.Login;
using SecurityMonitor.Identity.Application.Authentication.Register;

namespace SecurityMonitor.Identity.Api.Endpoints.Auth;

public static class AuthEndpoints
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost(
            "/auth/register",
            async (RegisterCommand command, RegisterHandler handler, CancellationToken cancellationToken) =>
            {
                var result = await handler.Handle(command,cancellationToken);

                return Results.Ok(result);
            });

        endpoints.MapPost(
            "/auth/login",
            async (LoginCommand command,LoginHandler handler,CancellationToken cancellationToken) =>
            {
                var result = await handler.Handle(command, cancellationToken);

                return Results.Ok(result);
            });

        return endpoints;
    }
}
