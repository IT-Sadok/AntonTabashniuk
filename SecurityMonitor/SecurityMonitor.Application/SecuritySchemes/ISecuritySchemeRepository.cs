using SecurityMonitor.Domain.Administrative;

namespace SecurityMonitor.Application.SecuritySchemes;

public interface ISecuritySchemeRepository
{
    Task<bool> ExistsAsync(string name, CancellationToken cancellationToken);

    Task<int> AddAsync(SecurityScheme securityScheme, CancellationToken cancellationToken);
}

