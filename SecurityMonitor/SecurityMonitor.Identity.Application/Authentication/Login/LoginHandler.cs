using SecurityMonitor.Identity.Application.ResultPattern;

namespace SecurityMonitor.Identity.Application.Authentication.Login;

public sealed class LoginHandler
{
    private readonly IIdentityService identityService;
    public LoginHandler(IIdentityService identityService)
    {
        this.identityService = identityService;
    }

    public async Task<Result> Handle(LoginCommand command, CancellationToken ct)
    {
        return await identityService.LoginAsync(command.Email, command.Password, ct);
    }
}
