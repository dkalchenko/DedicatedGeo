using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Core.Extensions;
using DedicatedGeo.Mono.Dtos.User;
using FluentValidation;

namespace DedicatedGeo.Mono.Core.User.Validators.Admin;

public class GetUsersAdminRequestValidator: AbstractValidator<GetUsersAdminRequest>
{
    public GetUsersAdminRequestValidator()
    {
        RuleFor(x => x.UserIds)
            .MustBeListGuids(1, 10)
            .When(x => x.UserIds != null);
        RuleFor(x => x.Role)
            .NotEmpty()
            .Must(x => OwnConstants.Roles.AllRoles.Contains(x))
            .When(x => x.Role != null);
    }
}