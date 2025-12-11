using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DedicatedGeo.Mono.Models.Device;

public class DeviceStatusConfiguration : IEntityTypeConfiguration<DeviceStatus>
{
    public void Configure(EntityTypeBuilder<DeviceStatus> builder)
    {
        builder.HasKey(e => e.DeviceStatusId);
        builder.HasIndex(e => e.DeviceId);
        builder.Property(e => e.BatteryLevel).IsRequired();
        builder.Property(e => e.IsInAlarm).IsRequired();
        builder.Property(e => e.IsButtonPressed).IsRequired();
        builder.Property(e => e.IsInCharge).IsRequired();
        builder.Property(e => e.IsGPSOnline).IsRequired();
    }
}