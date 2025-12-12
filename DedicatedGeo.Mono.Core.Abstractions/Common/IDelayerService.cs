namespace DedicatedGeo.Mono.Core.Abstractions.Common;

public interface IDelayerService
{
    public Task DelayAsync(int milliseconds, CancellationToken cancellationToken);
    public Task DelayAsync(long spanTicks, CancellationToken cancellationToken);
    public Task DayDelayAsync(CancellationToken cancellationToken);
}