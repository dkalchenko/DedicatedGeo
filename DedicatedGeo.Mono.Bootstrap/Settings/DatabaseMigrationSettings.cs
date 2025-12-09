using DedicatedGeo.Mono.Common.Extensions;
using DedicatedGeo.Mono.Dal.Abstractions;
using Microsoft.Extensions.Configuration;

namespace DedicatedGeo.Mono.Bootstrap.Settings;

public class DatabaseMigrationSettings : IDatabaseMigrationSettings
{
    public const string Position = "MySqlMigration";

    public DatabaseMigrationSettings(IConfiguration configuration)
    {
        Guard.ThrowIfNull(configuration, nameof(configuration));
        configuration.GetSection(Position).Bind(this);
    }

    public int MigrationsAttemptsCount { get; set; }
    public int MigrationsRetryDelaySec { get; set; }
}