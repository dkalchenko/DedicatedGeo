using DedicatedGeo.Mono.Dtos.Auth;
using FluentValidation;

namespace DedicatedGeo.Mono.Core.Auth.Validators;

public class PostAuthPublicRequestValidator: AbstractValidator<PostAuthLoginPublicRequest>
{
    public PostAuthPublicRequestValidator()
    {
        RuleFor(r => r.Email).NotEmpty();
        RuleFor(r => r.Password).NotEmpty();
    }
}