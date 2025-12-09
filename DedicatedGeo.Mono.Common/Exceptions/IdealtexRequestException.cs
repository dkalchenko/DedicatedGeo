using System.Net;

namespace DedicatedGeo.Mono.Common.Extensions;

public class IdealtexRequestException : HttpRequestException
{
    public IdealtexRequestException(string? message, Exception? inner, HttpStatusCode? statusCode, int errorCode) :
        base(message, inner, statusCode)
    {
        ErrorCode = errorCode;
    }

    public int ErrorCode { get; }
}