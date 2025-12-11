using DedicatedGeo.Mono.Dtos.Location;
using FluentValidation;

namespace DedicatedGeo.Mono.Core.Location;

public class DeleteLocationPointsAdminRequestValidator: AbstractValidator<DeleteLocationPointsAdminRequest>
{
    public DeleteLocationPointsAdminRequestValidator()
    {
        RuleFor(request => request.DeviceId).NotNull().NotEmpty();
    }
}