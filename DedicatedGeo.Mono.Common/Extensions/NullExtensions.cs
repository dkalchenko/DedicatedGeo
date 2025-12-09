namespace DedicatedGeo.Mono.Common;

public static class NullExtensions
{
    public static T ThrowIfNull<T>(this T? subject)
    {
        return subject ?? throw new ArgumentNullException();
    }
}