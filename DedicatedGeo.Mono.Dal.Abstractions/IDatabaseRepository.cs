using DedicatedGeo.Mono.Models;
using Microsoft.EntityFrameworkCore;

namespace DedicatedGeo.Mono.Dal.Abstractions;

public interface IDatabaseRepository
{
    DbSet<User> Users { get; }
    Task SaveChangesAsync(CancellationToken? cancellationToken);
}