using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DedicatedGeo.Mono.Models.Device;

public class DeviceAssignmentConfiguration: IEntityTypeConfiguration<DeviceAssignment>
{
    public void Configure(EntityTypeBuilder<DeviceAssignment> builder)
    {
        builder.HasKey(x => x.DeviceAssignmentId);
        builder.HasIndex(x => x.DeviceId);
        builder.HasIndex(x => x.UserId);
    }
}