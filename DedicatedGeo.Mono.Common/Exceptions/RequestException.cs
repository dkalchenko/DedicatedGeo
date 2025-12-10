using System.Net;

namespace DedicatedGeo.Mono.Common.Extensions;

public class RequestException : HttpRequestException
{
    public RequestException(string? message, Exception? inner, HttpStatusCode? statusCode, int errorCode) :
        base(message, inner, statusCode)
    {
        ErrorCode = errorCode;
    }

    public int ErrorCode { get; }
}