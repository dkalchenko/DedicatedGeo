using DedicatedGeo.Mono.Models;
using DedicatedGeo.Mono.Models.Device;
using DedicatedGeo.Mono.Models.Location;
using DedicatedGeo.Mono.Models.User;
using Microsoft.EntityFrameworkCore;

namespace DedicatedGeo.Mono.Dal.Abstractions;

public interface IDatabaseRepository
{
    DbSet<LocationPoint> LocationPoints { get; }
    DbSet<DeviceStatus> DeviceStatuses { get; }
    DbSet<Device> Devices { get; }
    DbSet<DeviceStatusHistory> DeviceStatusHistories { get; }
    DbSet<User> Users { get; }
    Task SaveChangesAsync(CancellationToken? cancellationToken);
    void RefreshTracker();
}