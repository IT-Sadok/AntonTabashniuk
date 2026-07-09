using SecurityMonitor.Domain.Administrative;

namespace SecurityMonitor.Application.SecuritySchemes;

public interface ISecuritySchemeRepository
{
    Task<bool> ExistsAsync(string name, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken);
    Task<int> AddAsync(SecurityScheme securityScheme, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int securitySchemeId, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(SecurityScheme securityScheme, CancellationToken cancellationToken);
    Task<SecurityScheme?> GetAsync(int securitySchemeId, CancellationToken cancellationToken);
    Task<List<SecurityScheme>> GetAllAsync(CancellationToken cancellationToken);
}

