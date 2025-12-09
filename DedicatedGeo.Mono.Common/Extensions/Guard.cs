namespace DedicatedGeo.Mono.Common.Extensions;

public static class Guard
{
    public static TSubject ThrowIfNull<TSubject>(TSubject? subject, string name)
    {
        if (subject == null) throw new ArgumentNullException(name);
        return subject;
    }

    public static IEnumerable<T> ThrowIfNullOrEmpty<T>(IEnumerable<T>? subject, string name)
    {
        var throwIfNullOrEmpty = ThrowIfNull(subject?.ToList(), name);
        if (throwIfNullOrEmpty.Empty()) throw new ArgumentException("Should not be empty", name);
        return throwIfNullOrEmpty;
    }
}