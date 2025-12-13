using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Core.Abstractions.Common;
using DedicatedGeo.Mono.Core.Extensions;
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
        var repository =
            scope.ServiceProvider
                .GetRequiredService<IDatabaseRepository>();

        var delayer = scope.ServiceProvider.GetRequiredService<IDelayerService>();
        var publisher = scope.ServiceProvider.GetRequiredService<IPublisher>();

        while (true)
        {
            try
            {
                var devices = await repository.Devices.ToListAsync(cancellationToken: stoppingToken);

                foreach (var devicesChunk in devices.Chunk(50))
                {

                    var changedStatuses = new List<Models.Device.DeviceStatus>();

                    foreach (var device in devicesChunk)
                    {
                        var devicesStatus = await repository.DeviceStatuses
                            .AsQueryable()
                            .Where(x => x.DeviceId == device.DeviceId)
                            .OrderByDescending(x => x.UpdatedAt)
                            .FirstOrDefaultAsync(cancellationToken: stoppingToken);

                        if (devicesStatus is null)
                        {
                            continue;
                        }

                        if (devicesStatus.IsDeviceOnline &&
                            !devicesStatus.UpdatedAt.IsWithinLastMinutes(OwnConstants
                                .DeviceIsInactivityStatusAfterInMinutes))
                        {
                            devicesStatus.IsDeviceOnline = false;
                            changedStatuses.Add(devicesStatus);
                        }
                    }

                    await repository.SaveChangesAsync(cancellationToken: stoppingToken);
                    var notificationItems = changedStatuses.Select(x => new ChangedDeviceStatusItem
                    {
                        NewValue = x.IsDeviceOnline.ToString(),
                        OldValue = true.ToString(),
                        StatusName = OwnConstants.DeviceStatusNames.IsDeviceOnline,
                        DeviceId = x.DeviceId,
                        ChangedAt = x.UpdatedAt
                    });

                    await publisher.Publish(new DeviceStatusesChangedNotification
                    {
                        ChangedDeviceStatuses = notificationItems,
                    }, stoppingToken);
                }
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