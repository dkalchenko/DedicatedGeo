using DedicatedGeo.Mono.Core.Abstractions.Common;

namespace DedicatedGeo.Mono.Core.Common;

public class DelayerService: IDelayerService
{
    public async Task DelayAsync(int milliseconds, CancellationToken cancellationToken)
    {
        await Task.Delay(milliseconds, cancellationToken);
    }
    
    public async Task DelayAsync(long ticks, CancellationToken cancellationToken)
    {
        await Task.Delay(new TimeSpan(ticks), cancellationToken);
    }
    
    public async Task DayDelayAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(new TimeSpan(TimeSpan.TicksPerDay), cancellationToken);
    }
}