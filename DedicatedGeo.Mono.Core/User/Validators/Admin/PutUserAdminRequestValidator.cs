using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Core.Extensions;
using DedicatedGeo.Mono.Dtos.User;
using FluentValidation;

namespace DedicatedGeo.Mono.Core.User.Validators.Admin;

public class PutUserAdminRequestValidator: AbstractValidator<PutUserAdminRequest>
{
    public PutUserAdminRequestValidator()
    {
        RuleFor(x => x.UserId).MustBeGuid();
        RuleFor(x => x.Email).NotEmpty();
        RuleFor(x => x.Password).NotEmpty().When(x => !string.IsNullOrEmpty(x.Password));
        RuleFor(x => x.Role).NotEmpty().Must(x => OwnConstants.Roles.AllRoles.Contains(x));
    }
}