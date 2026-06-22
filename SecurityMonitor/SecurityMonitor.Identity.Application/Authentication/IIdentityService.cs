using SecurityMonitor.Identity.Application.Authentication.Login;
using SecurityMonitor.Identity.Application.Common;

namespace SecurityMonitor.Identity.Application.Authentication;

public interface IIdentityService
{
    Task<Result<bool>> RegisterAsync(string email,string password, CancellationToken ct);
    Task<Result<LoginResponse>>  LoginAsync(string email,string password, CancellationToken ct);
}
