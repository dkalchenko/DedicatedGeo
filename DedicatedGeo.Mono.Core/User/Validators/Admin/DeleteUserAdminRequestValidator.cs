using DedicatedGeo.Mono.Core.Extensions;
using DedicatedGeo.Mono.Dtos.User;
using FluentValidation;

namespace DedicatedGeo.Mono.Core.User.Validators.Admin;

public class DeleteUserAdminRequestValidator: AbstractValidator<DeleteUserAdminRequest>
{
    public DeleteUserAdminRequestValidator()
    {
        RuleFor(x => x.UserId).MustBeGuid();
    }
}