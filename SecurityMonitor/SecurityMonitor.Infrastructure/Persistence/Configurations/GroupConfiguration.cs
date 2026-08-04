using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecurityMonitor.Infrastructure.Persistence.Entities;

namespace SecurityMonitor.Infrastructure.Persistence.Configurations;

public sealed class GroupConfiguration : IEntityTypeConfiguration<GroupEntity>
{
    public void Configure(EntityTypeBuilder<GroupEntity> builder)
    {
        builder.ToTable("groups");

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Device)
            .WithMany(x => x.Groups)
            .HasForeignKey(x => x.DeviceId);

        builder.Property(x => x.Name)
            .HasColumnName("name")
            .HasMaxLength(100)
            .IsRequired();

        builder.HasMany(x => x.Zones)
            .WithOne()
            .HasForeignKey(x => x.GroupId);

        builder.Property(x => x.State)
            .HasColumnName("state");
    }
}
