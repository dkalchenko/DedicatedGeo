using DedicatedGeo.Mono.Core.Extensions;
using DedicatedGeo.Mono.Dtos.User;
using FluentValidation;

namespace DedicatedGeo.Mono.Core.User.Validators.Admin;

public class GetUserAdminRequestValidator: AbstractValidator<GetUserAdminRequest>
{
    public GetUserAdminRequestValidator()
    {
        RuleFor(x => x.UserId).MustBeGuid();
    }
}