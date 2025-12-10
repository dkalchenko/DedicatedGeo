using DedicatedGeo.Mono.Models;
using DedicatedGeo.Mono.Models.Location;
using Microsoft.EntityFrameworkCore;

namespace DedicatedGeo.Mono.Dal.Abstractions;

public interface IDatabaseRepository
{
    DbSet<LocationPoint> LocationPoints { get; }
    Task SaveChangesAsync(CancellationToken? cancellationToken);
}