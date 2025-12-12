using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DedicatedGeo.Mono.Models.Device;

public class DeviceStatusHistoryConfiguration : IEntityTypeConfiguration<DeviceStatusHistory>
{
    public void Configure(EntityTypeBuilder<DeviceStatusHistory> builder)
    {
        builder.HasKey(e => e.DeviceStatusHistoryId);
        builder.HasIndex(e => new { e.DeviceId, e.StatusName, e.ChangedAt });
        builder.Property(e => e.StatusName).HasMaxLength(40);
        builder.Property(e => e.OldValue).IsRequired(false).HasMaxLength(40);
        builder.Property(e => e.NewValue).HasMaxLength(40);
    }
}