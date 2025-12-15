using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Dtos.User;
using FluentValidation;

namespace DedicatedGeo.Mono.Core.User.Validators.Admin;

public class PostUserAdminRequestValidator: AbstractValidator<PostUserAdminRequest>
{
    public PostUserAdminRequestValidator()
    {
        RuleFor(x => x.Email).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
        RuleFor(x => x.Role).NotEmpty().Must(x => OwnConstants.Roles.AllRoles.Contains(x));
    }
}