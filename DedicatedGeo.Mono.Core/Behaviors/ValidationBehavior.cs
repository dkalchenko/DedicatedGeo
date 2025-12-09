using FluentValidation;
using DedicatedGeo.Mono.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DedicatedGeo.Mono.Core.Behaviors;

public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IValidator<TRequest>? _validator;
    private readonly ILogger<ValidatorBehavior<TRequest, TResponse>> _logger;

    public ValidatorBehavior(ILogger<ValidatorBehavior<TRequest, TResponse>> logger, IValidator<TRequest>? validator = null)
    {
        _validator = validator;
        _logger = logger.ThrowIfNull();
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        if (_validator is not null) await _validator.ValidateAndThrowAsync(request, cancellationToken);
        return await next();
    }
}