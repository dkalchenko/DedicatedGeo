using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DedicatedGeo.Mono.Models.Location;

public class LocationPointConfiguration : IEntityTypeConfiguration<LocationPoint>
{
    public void Configure(EntityTypeBuilder<LocationPoint> builder)
    {
        builder.HasKey(e => e.LocationPointId);
        builder.Property(e => e.Point).IsRequired();
        builder.HasIndex(e => e.CreatedAt);
        builder.HasIndex(e => new { e.DeviceId, e.CreatedAt });
    }
}