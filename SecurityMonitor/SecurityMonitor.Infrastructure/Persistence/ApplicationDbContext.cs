using Microsoft.EntityFrameworkCore;
using SecurityMonitor.Infrastructure.Persistence.Entities;

namespace SecurityMonitor.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public DbSet<SecuritySchemeEntity> SecuritySchemes => Set<SecuritySchemeEntity>();
    public DbSet<DeviceEntity> Devices => Set<DeviceEntity>();
    public DbSet<GroupEntity> Groups => Set<GroupEntity>();
    public DbSet<ZoneEntity> Zones => Set<ZoneEntity>();
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}