using SecurityMonitor.Identity.Application.ResultPattern;

namespace SecurityMonitor.Identity.Application.Authentication.Register;

public sealed class RegisterHandler
{
    private readonly IIdentityService identityService;

    public RegisterHandler(IIdentityService identityService) 
    {
        this.identityService = identityService;
    }

    public async Task<Result> Handle(RegisterCommand command, CancellationToken ct)
    {
        return await identityService.RegisterAsync(command.Email, command.Password, ct);
    }
}
