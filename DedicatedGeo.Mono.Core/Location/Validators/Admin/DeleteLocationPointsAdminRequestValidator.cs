using DedicatedGeo.Mono.Core.Extensions;
using DedicatedGeo.Mono.Dtos.Location;
using FluentValidation;

namespace DedicatedGeo.Mono.Core.Location;

public class DeleteLocationPointsAdminRequestValidator: AbstractValidator<DeleteLocationPointsAdminRequest>
{
    public DeleteLocationPointsAdminRequestValidator()
    {
        RuleFor(request => request.DeviceId).MustBeGuid();
        RuleFor(request => request.DeviceId).MustBeGuid();
    }
}