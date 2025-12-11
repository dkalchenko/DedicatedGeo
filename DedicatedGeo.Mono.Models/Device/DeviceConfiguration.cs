using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DedicatedGeo.Mono.Models.Device;

public class DeviceConfiguration : IEntityTypeConfiguration<Device>
{
    public void Configure(EntityTypeBuilder<Device> builder)
    {
        builder.HasKey(d => d.DeviceId);
        builder.HasIndex(d => d.IMEI);
        builder.HasIndex(d => d.QuarantineUntil);
        
        builder.Property(d => d.IMEI)
            .HasMaxLength(16)
            .IsRequired();
        
        builder.HasOne(x => x.DeviceStatus)
            .WithOne()
            .HasForeignKey<DeviceStatus>(x => x.DeviceId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(x => x.LocationPoints)
            .WithOne()
            .HasForeignKey(x => x.DeviceId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}