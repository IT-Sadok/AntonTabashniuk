using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecurityMonitor.Infrastructure.Persistence.Entities;

namespace SecurityMonitor.Infrastructure.Persistence.Configurations;

public sealed class ZoneConfiguration : IEntityTypeConfiguration<ZoneEntity>
{
    public void Configure(EntityTypeBuilder<ZoneEntity> builder)
    {
        builder.ToTable("zones");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.GroupId)
            .HasColumnName("group_id");

        builder.Property(x => x.Name)
            .HasColumnName("name")
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.State)
            .HasColumnName("state");

        builder.Property(x => x.Type)
            .HasColumnName("type");

        builder.HasOne(x => x.Device)
            .WithMany(x => x.Zones)
            .HasForeignKey(x => x.DeviceId);

        builder.HasOne(x => x.Group)
            .WithMany(x => x.Zones)
            .HasForeignKey(x => x.GroupId);
    }
}
