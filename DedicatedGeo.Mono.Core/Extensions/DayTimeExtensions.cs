namespace DedicatedGeo.Mono.Core.Extensions;

public static class DayTimeExtensions
{
    
    public static bool IsWithinLast(this DateTime? dateTime, TimeSpan span)
    {
        if (!dateTime.HasValue) return false;
        var dtUtc = dateTime.Value;
        var now = DateTime.UtcNow;
        return dtUtc <= now && (now - dtUtc) <= span;
    }

    public static bool IsWithinLastMinutes(this DateTime dateTime, int minutes) =>
        IsWithinLast(dateTime, TimeSpan.FromMinutes(minutes));
}