using DedicatedGeo.Mono.Core.Extensions;
using DedicatedGeo.Mono.Dtos.Location;
using FluentValidation;

namespace DedicatedGeo.Mono.Core.Location;

public class GetLocationPointsAdminRequestValidator: AbstractValidator<GetLocationPointsAdminRequest>
{
    public GetLocationPointsAdminRequestValidator()
    {
        RuleFor(request => request.UserId).MustBeGuid();
        RuleFor(request => request.DeviceId).MustBeGuid();
    }
}