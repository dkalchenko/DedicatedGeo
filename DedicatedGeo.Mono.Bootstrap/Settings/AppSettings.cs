using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Common.Extensions;
using Microsoft.Extensions.Configuration;

namespace DedicatedGeo.Mono.Bootstrap.Settings;

public class AppSettings
{
    public const string Position = "App";

    public AppSettings(IConfiguration configuration)
    {
        Guard.ThrowIfNull(configuration, nameof(configuration));
        configuration.GetSection(Position).Bind(this);
        Env = Environment.GetEnvironmentVariable(OwnConstants.EnvironmentKeys.AspnetcoreEnvironment) ?? Env;
    }

    public string Env { get; set; } = null!;
    public bool isProd => Env == "Production";
    public bool isDev => Env == "Development";
}