using SecurityMonitor.Identity.Application.Authentication.Login;
using SecurityMonitor.Identity.Application.Authentication.Register;

namespace SecurityMonitor.Identity.Api.Endpoints.Auth;

public static class AuthEndpoints
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup(AuthRoutes.Base);

        group.MapPost(AuthRoutes.Register, Register());

        group.MapPost(AuthRoutes.Login, Login());

        return endpoints;
    }
    private static Func<RegisterCommand, RegisterHandler, CancellationToken, Task<IResult>> Register()
    {
        return async (RegisterCommand command, RegisterHandler handler, CancellationToken cancellationToken) =>
        {
            var result = await handler.Handle(command, cancellationToken);

            return Results.Ok(result);
        };
    }

    private static Func<LoginCommand, LoginHandler, CancellationToken, Task<IResult>> Login()
    {
        return async (LoginCommand command, LoginHandler handler, CancellationToken cancellationToken) =>
        {
            var result = await handler.Handle(command, cancellationToken);

            return Results.Ok(result);
        };
    }
}
