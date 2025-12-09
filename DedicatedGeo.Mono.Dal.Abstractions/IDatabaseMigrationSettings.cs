namespace DedicatedGeo.Mono.Dal.Abstractions;

public interface IDatabaseMigrationSettings
{
    int MigrationsAttemptsCount { get; }
    int MigrationsRetryDelaySec { get; }
}