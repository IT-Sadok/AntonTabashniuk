using SecurityMonitor.Identity.Application.Common;

namespace SecurityMonitor.Identity.Application.Authentication.Login;

public sealed class LoginHandler
{
    private readonly IIdentityService identityService;
    public LoginHandler(IIdentityService identityService)
    {
        this.identityService = identityService;
    }

    public async Task<Result<LoginResponse>> Handle(LoginCommand command, CancellationToken ct)
    {
        return await identityService.LoginAsync(command.Email, command.Password, ct);
    }
}
