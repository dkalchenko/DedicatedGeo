using System.Net;

namespace DedicatedGeo.Mono.Dtos;

public class ExceptionResponse
{
    public HttpStatusCode StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
    public string? StackTrace { get; set; } = string.Empty;
    public int ErrorCode { get; set; }
}