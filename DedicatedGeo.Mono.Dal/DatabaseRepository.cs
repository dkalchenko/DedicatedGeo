using System;
using System.Threading;
using System.Threading.Tasks;
using EntityFramework.Exceptions.MySQL.Pomelo;
using DedicatedGeo.Mono.Dal.Abstractions;
using DedicatedGeo.Mono.Models;
using DedicatedGeo.Mono.Models.Device;
using DedicatedGeo.Mono.Models.Location;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;

namespace DedicatedGeo.Mono.Dal;

public class DatabaseRepository : DbContext, IDatabaseRepository
{
    public DatabaseRepository(DbContextOptions options) : base(options)
    {
        _ = options ?? throw new ArgumentNullException($"{nameof(options)} is not defined");
    }

    public DbSet<LocationPoint> LocationPoints { get; init; }
    public DbSet<DeviceStatus> DeviceStatuses { get; init; }
    public DbSet<Device> Devices { get; init; }
    public DbSet<DeviceStatusHistory> DeviceStatusHistories { get; init; }

    public virtual async Task SaveChangesAsync(CancellationToken? cancellationToken)
    {
        await base.SaveChangesAsync(cancellationToken.GetValueOrDefault());
    }

    public void RefreshTracker()
    {
        ChangeTracker.Clear();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseExceptionProcessor();
        base.OnConfiguring(optionsBuilder);
    }

    protected sealed override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IAssemblyModelsMarker).Assembly);
        var provider = Database.ProviderName ?? string.Empty;

        if (provider.Contains("Sqlite", StringComparison.OrdinalIgnoreCase))
        {
            var wkbWriter = new WKBWriter(); // writes byte[]
            var wkbReader = new WKBReader();

            var converter = new ValueConverter<Point, byte[]>(
                p => p == null ? null : wkbWriter.Write(p),
                b => b == null ? null : wkbReader.Read(b) as Point);

            modelBuilder.Entity<LocationPoint>()
                .Property(e => e.Point)
                .HasConversion(converter)
                .HasColumnType("BLOB");
        }
    }
}