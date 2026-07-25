using Microsoft.EntityFrameworkCore;
using SecurityMonitor.Application.SecuritySchemes;
using SecurityMonitor.Domain.Administrative;
using SecurityMonitor.Domain.Devices;
using SecurityMonitor.Infrastructure.Persistence.Mappers;
using System.Reflection.Metadata.Ecma335;

namespace SecurityMonitor.Infrastructure.Persistence.Repositories;

public class SecuritySchemeRepository : ISecuritySchemeRepository
{
    private readonly ApplicationDbContext _dbContext; 
    public SecuritySchemeRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<SecurityScheme>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.SecuritySchemes
            .Include(p => p.Device)
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
            .FirstOrDefaultAsync(x => x.Id == securityScheme.Id, cancellationToken);

        if (securitySchemeEntity is null) 
            return false;
        
        securitySchemeEntity.Name = securityScheme.Name;
        securitySchemeEntity.Description = securityScheme.Description;

        if (securitySchemeEntity.Device is null)
            return false;

        securitySchemeEntity.Device.SerialNumber = securityScheme.Device.SerialNumber;
        securitySchemeEntity.Device.DeviceType = securityScheme.Device.DeviceType;
        securitySchemeEntity.Device.DeviceState = securityScheme.Device.DeviceState;

        return true;
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
