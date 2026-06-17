using SecurityMonitor.Identity.Application.ResultPattern;

namespace SecurityMonitor.Identity.Application.Authentication;

public interface IIdentityService
{
    Task<Result> RegisterAsync(string email,string password, CancellationToken ct);
    Task<Result> LoginAsync(string email,string password, CancellationToken ct);
}
