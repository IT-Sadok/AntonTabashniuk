using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecurityMonitor.Infrastructure.Persistence.Entities;

namespace SecurityMonitor.Infrastructure.Persistence.Configurations;

public sealed class SecuritySchemeConfiguration : IEntityTypeConfiguration<SecuritySchemeEntity>
{
    public void Configure(EntityTypeBuilder<SecuritySchemeEntity> builder)
    {
        builder.ToTable("security_schemes");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasColumnName("name")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasColumnName("description")
            .HasMaxLength(1000);
    }
}