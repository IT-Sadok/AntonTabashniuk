using SecurityMonitor.Identity.Application.Common;

namespace SecurityMonitor.Identity.Application.Authentication.Register;

public sealed class RegisterHandler
{
    private readonly IIdentityService identityService;

    public RegisterHandler(IIdentityService identityService) 
    {
        this.identityService = identityService;
    }

    public async Task<Result<bool>> Handle(RegisterCommand command, CancellationToken ct)
    {
        return await identityService.RegisterAsync(command.Email, command.Password, ct);
    }
}
