using Microsoft.EntityFrameworkCore;
using SecurityMonitor.Application.SecuritySchemes;
using SecurityMonitor.Domain.Administrative;
using SecurityMonitor.Infrastructure.Persistence.Mappers;

namespace SecurityMonitor.Infrastructure.Persistence.Repositories;

public class SecuritySchemeRepository : ISecuritySchemeRepository
{
    private readonly ApplicationDbContext _dbContext; 
    public SecuritySchemeRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<int> AddAsync(SecurityScheme securityScheme, CancellationToken ct)
    {
        var entity = securityScheme.ToEntity();
        
        await _dbContext.SecuritySchemes.AddAsync(entity, ct);
        await _dbContext.SaveChangesAsync(ct);

        return entity.Id;
    }

    public Task<bool> ExistsAsync(string name, CancellationToken ct)
    {
        return _dbContext.SecuritySchemes.AnyAsync(x => x.Name == name, ct);
    }
}
