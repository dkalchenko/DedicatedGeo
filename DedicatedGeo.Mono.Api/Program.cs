using DedicatedGeo.Mono.Bootstrap;
using DedicatedGeo.Mono.Bootstrap.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using NLog;
using NLog.Extensions.Logging;
using Prometheus;
using System;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json");
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json");
builder.Logging.ClearProviders();
var loggerFactory = LoggerFactory.Create(conf =>
{
    conf.AddNLog(new NLogProviderOptions { RemoveLoggerFactoryFilter = true });
});
builder.Services.AddSingleton(loggerFactory);
builder.Services.AddSingleton(builder.Configuration);
builder.Services.ConfigureServices(builder.Configuration);
builder.Services.AddSettings();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var settings = new JwtBearerTokenSettings(builder.Configuration);
        options.TokenValidationParameters = settings.GenerateTokenValidationParameters();
        // Read token from cookie named "AuthToken"
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                if (context.Request.Cookies.TryGetValue("AuthToken", out var token) && !string.IsNullOrEmpty(token))
                {
                    context.Token = token;
                }
                return Task.CompletedTask;
            }
        };
    });

var app = builder.Build();

var fh = new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
    RequireHeaderSymmetry = false,
    ForwardLimit = 1
};

app.UseForwardedHeaders(fh);
app.Configuration.ConfigureLogging();
app.UseHttpMetrics();
app.AddMiddlewares();
app.UseCors();

app.UseHttpsRedirection();
app.MapMetrics();
app.MapControllers();

await app.Services.ConfigureAsync();
await app.RunAsync();
LogManager.Shutdown();

public partial class Program
{
}