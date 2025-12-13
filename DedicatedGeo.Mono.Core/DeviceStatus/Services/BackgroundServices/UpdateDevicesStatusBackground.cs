using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Core.Abstractions.Common;
using DedicatedGeo.Mono.Dal.Abstractions;
using DedicatedGeo.Mono.Entities.DeviceStatus;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DedicatedGeo.Mono.Core.DeviceStatus.Services.BackgroundServices;

public class UpdateDevicesStatusBackground: BackgroundService
{
    private readonly ILogger<UpdateDevicesStatusBackground> _logger;
    private readonly IServiceProvider _services;

    public UpdateDevicesStatusBackground(ILogger<UpdateDevicesStatusBackground> logger, IServiceProvider services)
    {
        _logger = logger ?? throw new ArgumentNullException($"{nameof(logger)} was not defined");
        _services = services ?? throw new ArgumentNullException($"{nameof(services)} was not defined");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogDebug("UpdateDevicesStatusBackground is starting");

        try
        {
            await DoAsync(stoppingToken);
        }
        catch (OperationCanceledException)
        {
            _logger.LogDebug("UpdateDevicesStatusBackground is stopping");
        }
       
    }

    private async Task DoAsync(CancellationToken stoppingToken)
    {
        await using var scope = _services.CreateAsyncScope();

        var delayer = scope.ServiceProvider.GetRequiredService<IDelayerService>();
        var publisher = scope.ServiceProvider.GetRequiredService<IPublisher>();

        var repository =
            scope.ServiceProvider
                .GetRequiredService<IDatabaseRepository>();
        
        while (true)
        {
            try
            {
                var cutoff = DateTime.UtcNow.AddMinutes(-2);

                var devicesStatus = await repository.DeviceStatuses
                    .Where(x => x.IsDeviceOnline && x.UpdatedAt < cutoff)
                    .OrderByDescending(x => x.UpdatedAt)
                    .ToListAsync(cancellationToken: stoppingToken);

                var notificationItems = new List<ChangedDeviceStatusItem>();

                foreach (var deviceStatus in devicesStatus)
                {
                     deviceStatus.IsDeviceOnline = false;
                     deviceStatus.UpdatedAt = DateTime.UtcNow;

                     notificationItems.Add(new ChangedDeviceStatusItem
                     {
                         NewValue = deviceStatus.IsDeviceOnline.ToString(),
                         OldValue = true.ToString(),
                         StatusName = OwnConstants.DeviceStatusNames.IsDeviceOnline,
                         DeviceId = deviceStatus.DeviceId,
                         ChangedAt = deviceStatus.UpdatedAt
                     });
                }

                if (notificationItems.Count <= 0)
                {
                    continue;
                }
                
                await repository.SaveChangesAsync(cancellationToken: stoppingToken);

                repository.RefreshTracker();
                
                await publisher.Publish(new DeviceStatusesChangedNotification
                {
                    ChangedDeviceStatuses = notificationItems,
                }, stoppingToken);
                
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception from UpdateDevicesStatusBackground");
            }
            finally
            {
                await delayer.DelayAsync(OwnConstants.UpdateDeviceStatusBackgroundDelayInMillisecond, stoppingToken);
            }
        }
        // ReSharper disable once FunctionNeverReturns
    }

    
    
    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogDebug("UpdateDevicesStatusBackground is stopped");

        await base.StopAsync(stoppingToken);
    }
}