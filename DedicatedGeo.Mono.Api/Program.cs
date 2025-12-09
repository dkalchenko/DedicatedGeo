using System.Net;
using DedicatedGeo.Mono.Bootstrap;
using DedicatedGeo.Mono.Bootstrap.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.FileProviders;
using NLog;
using NLog.Extensions.Logging;
using Prometheus;
using IPNetwork = Microsoft.AspNetCore.HttpOverrides.IPNetwork;

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


var app = builder.Build();

var fh = new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
    RequireHeaderSymmetry = false,
    ForwardLimit = 1
};
fh.KnownProxies.Clear();
fh.KnownNetworks.Add(new IPNetwork(IPAddress.Parse("0.0.0.0"), 0));
fh.KnownNetworks.Add(new IPNetwork(IPAddress.Parse("::"), 0));

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