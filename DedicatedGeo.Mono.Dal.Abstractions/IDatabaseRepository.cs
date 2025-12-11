using DedicatedGeo.Mono.Models;
using DedicatedGeo.Mono.Models.Device;
using DedicatedGeo.Mono.Models.Location;
using Microsoft.EntityFrameworkCore;

namespace DedicatedGeo.Mono.Dal.Abstractions;

public interface IDatabaseRepository
{
    DbSet<LocationPoint> LocationPoints { get; }
    DbSet<DeviceStatus> DeviceStatuses { get; }
    DbSet<Device> Devices { get; }
    Task SaveChangesAsync(CancellationToken? cancellationToken);
}