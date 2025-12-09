namespace DedicatedGeo.Mono.Common.Extensions;

public static class NullValidatorExtensions
{
    public static bool Empty<T>(this IEnumerable<T?>? enumerable)
    {
        return !(enumerable?.Any() ?? false);
    }
}