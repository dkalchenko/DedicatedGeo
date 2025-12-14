using DedicatedGeo.Mono.Dtos.User;
using FluentValidation;

namespace DedicatedGeo.Mono.Core.User.Validators.Admin;

public class PostUserAdminRequestValidator: AbstractValidator<PostUserAdminRequest>
{
    public PostUserAdminRequestValidator()
    {
        RuleFor(x => x.Email).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}