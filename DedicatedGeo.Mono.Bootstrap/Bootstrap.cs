using DedicatedGeo.Mono.Bootstrap.Middlewares;
using DedicatedGeo.Mono.Bootstrap.Settings;
using FluentValidation;
using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Common.Extensions;
using DedicatedGeo.Mono.Core;
using DedicatedGeo.Mono.Core.Abstractions.Auth.Services;
using DedicatedGeo.Mono.Core.Abstractions.Common;
using DedicatedGeo.Mono.Core.Abstractions.Device.Services;
using DedicatedGeo.Mono.Core.Abstractions.DeviceAssignment;
using DedicatedGeo.Mono.Core.Abstractions.Settings;
using DedicatedGeo.Mono.Core.Abstractions.User.Services;
using DedicatedGeo.Mono.Core.Auth.Services;
using DedicatedGeo.Mono.Core.Behaviors;
using DedicatedGeo.Mono.Core.Common;
using DedicatedGeo.Mono.Core.Device.Services;
using DedicatedGeo.Mono.Core.DeviceAssignment.Services;
using DedicatedGeo.Mono.Core.DeviceStatus.Services.BackgroundServices;
using DedicatedGeo.Mono.Core.User.Services;
using DedicatedGeo.Mono.Dal;
using DedicatedGeo.Mono.Dal.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NLog;
using NLog.Extensions.Logging;

namespace DedicatedGeo.Mono.Bootstrap;

public static class Bootstrap
{
    public static IServiceCollection ConfigureServices(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {

        serviceCollection.AddMemoryCache();

        serviceCollection.AddScoped<IDelayerService, DelayerService>();
        serviceCollection.AddScoped<IUsersServices, UsersService>();
        serviceCollection.AddScoped<ITokenService, TokenService>();
        serviceCollection.AddScoped<IDeviceService, DeviceService>();
        serviceCollection.AddScoped<IDeviceAssignmentService, DeviceAssignmentService>();
        serviceCollection.AddHostedService<UpdateDevicesStatusBackground>();
        
        serviceCollection.AddDbContext<IDatabaseRepository, DatabaseRepository>(
            GetDbContextOptionsAction());
        new List<Type>
            {
                typeof(ValidatorBehavior<,>)
            }
            .ForEach(behavior => { serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), behavior); });

        serviceCollection.AddTransient<IDatabaseInitializer, DatabaseInitializer>();

        serviceCollection.AddValidatorsFromAssembly(typeof(ICoreMark).Assembly);
        serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ICoreMark).Assembly));
        serviceCollection.AddHttpClient();
        serviceCollection.RemoveAll<IHttpMessageHandlerBuilderFilter>();
        serviceCollection.AddCors(options =>
        {
            options.AddDefaultPolicy(
                policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .SetIsOriginAllowedToAllowWildcardSubdomains();
                });
        });
        serviceCollection.AddMvc()
            .AddNewtonsoftJson(options =>
            {
                // reuse your JsonConvert.DefaultSettings if set
                ConfigureJson(options.SerializerSettings);
            })
            .AddHybridModelBinder();

        ConfigureValidator();

        return serviceCollection;
    }

    public static async Task ConfigureAsync(this IServiceProvider serviceProvider)
    {
        await using var scope = serviceProvider.CreateAsyncScope();
        var databaseInitializer = scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>();
        await databaseInitializer.InitDatabaseAsync();
    }

    public static IServiceCollection AddDbContext<TInt, TContext>(this IServiceCollection services,
        Action<IServiceProvider, DbContextOptionsBuilder> optionsAction)
        where TContext : DbContext, TInt
        where TInt : class
    {
        services.AddDbContext<TContext>(optionsAction);
        services.AddScoped<TInt, TContext>();

        return services;
    }
    
    public static void ConfigureLogging(this IConfiguration configuration)
    {
        Guard.ThrowIfNull(configuration, nameof(configuration));
        LogManager.Configuration = new NLogLoggingConfiguration(configuration.GetSection("NLog"));
        GlobalDiagnosticsContext.Set("LogFolder",
            Environment.GetEnvironmentVariable(OwnConstants.EnvironmentKeys.LogFolder) ?? @"c:\Logs\Idealtex");
    }

    private static Action<IServiceProvider, DbContextOptionsBuilder> GetDbContextOptionsAction()
    {
        return (serviceProvider, options) =>
        {
            var settings = serviceProvider.GetRequiredService<MysqlSettings>();

            options.UseSqlite(settings.ConnectionString);

            options.EnableDetailedErrors();
        };
    }

    private static void ConfigureJson(JsonSerializerSettings settings)
    {
        settings.DateFormatString = "yyyy-MM-dd'T'HH:mm:sss'Z'";
        settings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
        settings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
        settings.Formatting = Formatting.None;
        settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    }

    private static void ConfigureValidator()
    {
        ValidatorOptions.Global.DefaultClassLevelCascadeMode =
            ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;
    }

    public static void AddSettings(this IServiceCollection services)
    {
        services.AddSingleton<AppSettings>();
        services.AddSingleton<IDatabaseMigrationSettings, DatabaseMigrationSettings>();
        services.AddSingleton<MysqlSettings>();
        services.AddSingleton<IJwtBearerTokenSettings, JwtBearerTokenSettings>();
    }

    public static void AddMiddlewares(this IApplicationBuilder services)
    {
        services.UseMiddleware<ErrorHandlerMiddleware>();
    }
}