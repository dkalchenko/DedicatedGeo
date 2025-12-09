using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Common.Extensions;
using Microsoft.Extensions.Configuration;

namespace DedicatedGeo.Mono.Bootstrap.Settings;

public class MysqlSettings
{
    public const string Position = "Mysql";

    public MysqlSettings(IConfiguration configuration)
    {
        Guard.ThrowIfNull(configuration, nameof(configuration));
        configuration.GetSection(Position).Bind(this);
        ConnectionString =
            Environment.GetEnvironmentVariable(OwnConstants.EnvironmentKeys.MySqlConnectionString) ??
            ConnectionString.ThrowIfNull();
    }

    public string ConnectionString { get; set; }
    public string Version { get; set; }
}