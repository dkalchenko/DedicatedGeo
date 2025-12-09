using EntityFramework.Exceptions.MySQL.Pomelo;
using DedicatedGeo.Mono.Dal.Abstractions;
using DedicatedGeo.Mono.Models;
using Microsoft.EntityFrameworkCore;

namespace DedicatedGeo.Mono.Dal;

public class DatabaseRepository : DbContext, IDatabaseRepository
{
    public DatabaseRepository(DbContextOptions options) : base(options)
    {
        _ = options ?? throw new ArgumentNullException($"{nameof(options)} is not defined");
    }

    public DbSet<User> Users { get; init; }

    public virtual async Task SaveChangesAsync(CancellationToken? cancellationToken)
    {
        await base.SaveChangesAsync(cancellationToken.GetValueOrDefault());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseExceptionProcessor();
        base.OnConfiguring(optionsBuilder);
    }

    protected sealed override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IAssemblyModelsMarker).Assembly);
    }
}