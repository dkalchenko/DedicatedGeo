using System.Net;
using DedicatedGeo.Mono.Common.Extensions;

namespace DedicatedGeo.Mono.Common.Exceptions;

public class ExceptionMaker
{
    private readonly HttpStatusCode _statusCode;
    private int _errorCode;
    private Exception? _exception;

    private string _message;

    public ExceptionMaker(string message, HttpStatusCode statusCode, int errorCode = 1)
    {
        _message = Guard.ThrowIfNull(message, nameof(message));
        _statusCode = Guard.ThrowIfNull(statusCode, nameof(statusCode));
        _errorCode = errorCode;
    }

    public ExceptionMaker FormatMessage(params object?[] strings)
    {
        _message = _message.Format(strings);
        return this;
    }

    public ExceptionMaker WithInnerException(Exception ex)
    {
        _exception = ex;
        return this;
    }

    public ExceptionMaker WithErrorCode(int errorCode)
    {
        _errorCode = errorCode;
        return this;
    }


    public IdealtexRequestException GetException()
    {
        return new IdealtexRequestException(
            _message,
            _exception,
            _statusCode,
            _errorCode
        );
    }
}