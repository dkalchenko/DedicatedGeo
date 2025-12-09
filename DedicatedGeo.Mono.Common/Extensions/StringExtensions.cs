using System.Security.Cryptography;
using System.Text;
using BCrypt.Net;
using BC = BCrypt.Net.BCrypt;

namespace DedicatedGeo.Mono.Common.Extensions;

public static class StringExtensions
{
    public static string Format(this string source, params object?[] values)
    {
        return string.Format(source, Guard.ThrowIfNull(values, nameof(values)));
    }

    public static string FirstCharToUpper(this string input)
    {
        return input switch
        {
            null => throw new ArgumentNullException(nameof(input)),
            "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
            _ => string.Concat(input[0].ToString().ToUpper(), input.AsSpan(1))
        };
    }

    public static Guid ToGuid(this string source)
    {
        return Guid.Parse(source);
    }

    public static string ToPasswordHash(this string source)
    {
        return BC.HashPassword(source);
    }

    public static bool VerifyPasswordHash(this string password, string source)
    {
        password.ThrowIfNull();
        source.ThrowIfNull();
        return BC.Verify(password, source);
    }

    public static Stream ToStream(this string source)
    {
        var stream = new MemoryStream();
        var w = new StreamWriter(stream);
        w.Write(source);
        w.Flush();
        stream.Position = 0;
        return stream;
    }

    public static byte[] FromBase64(this string source)
    {
        return Convert.FromBase64String(source);
    }

    public static string ToBase64(this byte[] source)
    {
        return Convert.ToBase64String(source);
    }
}