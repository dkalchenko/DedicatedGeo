using DedicatedGeo.Mono.Dtos.Location;
using FluentValidation;

namespace DedicatedGeo.Mono.Core.Location;

public class GetLocationPointsAdminRequestValidator: AbstractValidator<GetLocationPointsAdminRequest>
{
    public GetLocationPointsAdminRequestValidator()
    {
        RuleFor(request => request.DeviceId).NotNull().NotEmpty();
    }
}