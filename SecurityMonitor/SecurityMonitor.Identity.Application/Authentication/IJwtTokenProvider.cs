namespace SecurityMonitor.Identity.Application.Authentication;

public interface IJwtTokenProvider
{
    string GenerateToken(string userId, string email);
}
