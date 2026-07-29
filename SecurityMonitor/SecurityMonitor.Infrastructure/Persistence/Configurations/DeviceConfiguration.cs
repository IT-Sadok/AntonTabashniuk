using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecurityMonitor.Infrastructure.Persistence.Entities;

namespace SecurityMonitor.Infrastructure.Persistence.Configurations;

public sealed class DeviceConfiguration : IEntityTypeConfiguration<DeviceEntity>
{
    public void Configure(EntityTypeBuilder<DeviceEntity> builder)
    {
        builder.ToTable("devices");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.SerialNumber)
            .HasColumnName("serial_number")
            .HasMaxLength(128)
            .IsRequired();

        builder.HasIndex(x => x.SerialNumber)
            .IsUnique();
        
        builder.Property(x => x.DeviceType)
            .HasColumnName("device_type");

        builder.Property(x => x.DeviceState)
            .HasColumnName("device_state");

        builder.Property(x => x.SecuritySchemeId)
            .HasColumnName("security_scheme_id");

        builder.HasOne(x => x.SecurityScheme)
            .WithOne(x => x.Device)
            .HasForeignKey<DeviceEntity>(x => x.SecuritySchemeId);

        builder.HasMany(x => x.Groups)
            .WithOne(x => x.Device)
            .HasForeignKey(x => x.DeviceId);

        builder.HasMany(x => x.Zones)
            .WithOne(x => x.Device)
            .HasForeignKey(x => x.DeviceId);
    }
}
