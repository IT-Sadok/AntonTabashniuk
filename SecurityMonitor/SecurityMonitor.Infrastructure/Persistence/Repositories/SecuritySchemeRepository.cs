using Microsoft.EntityFrameworkCore;
using SecurityMonitor.Application.SecuritySchemes;
using SecurityMonitor.Domain.Administrative;
using SecurityMonitor.Infrastructure.Persistence.Mappers;
using SecurityMonitor.Infrastructure.Persistence.Repositories.Helpers;

namespace SecurityMonitor.Infrastructure.Persistence.Repositories;

public class SecuritySchemeRepository : ISecuritySchemeRepository
{
    private readonly ApplicationDbContext _dbContext; 
    public SecuritySchemeRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<int> GetTotalCountAsync(CancellationToken cancellationToken) 
    {
        return await _dbContext.SecuritySchemes.CountAsync(cancellationToken);
    }
    public async Task<List<SecurityScheme>> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken)
    {
        return await _dbContext.SecuritySchemes
            .Include(x => x.Device)
            .OrderBy(x => x.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(x => x.ToDomain())
            .ToListAsync(cancellationToken);
    }

    public async Task<SecurityScheme?> GetAsync(int securitySchemeId, CancellationToken cancellationToken)
    {
        return await _dbContext.SecuritySchemes
            .Include(p => p.Device)
            .Where(x => x.Id == securitySchemeId)
            .Select(x => x.ToDomain())
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> DeleteAsync(int securitySchemeId, CancellationToken cancellationToken)
    {
        return await _dbContext.SecuritySchemes
            .Where(x => x.Id == securitySchemeId)
            .ExecuteDeleteAsync(cancellationToken) > 0;
    }

    public async Task<bool> UpdateAsync(SecurityScheme securityScheme, CancellationToken cancellationToken)
    {
        var securitySchemeEntity = await _dbContext.SecuritySchemes
            .Include(p => p.Device)
                .ThenInclude(x => x.Zones)
            .FirstOrDefaultAsync(x => x.Id == securityScheme.Id, cancellationToken);

        if (securitySchemeEntity is null) 
            return false;

        securitySchemeEntity.Name = securityScheme.Name;
        securitySchemeEntity.Description = securityScheme.Description;

        return UpdateDeviceHelper.UpdateDevice(securityScheme.Device, securitySchemeEntity.Device);
    }

    public async Task<int> AddAsync(SecurityScheme securityScheme, CancellationToken ct)
    {
        var entity = securityScheme.ToEntity();
        
        await _dbContext.SecuritySchemes.AddAsync(entity, ct);
        await SaveChangesAsync(ct);

        return entity.Id;
    } 
    public async Task<bool> ExistsAsync(string name, CancellationToken ct)
    {
        return await _dbContext.SecuritySchemes.AnyAsync(x => x.Name == name, ct);
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken ct)
    {
        return await _dbContext.SecuritySchemes.AnyAsync(x => x.Id == id, ct);
    }

    public Task SaveChangesAsync(CancellationToken ct)
    {
        return _dbContext.SaveChangesAsync(ct);
    }
}
