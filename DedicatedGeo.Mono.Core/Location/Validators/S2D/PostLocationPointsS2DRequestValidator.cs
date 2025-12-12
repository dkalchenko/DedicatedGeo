using DedicatedGeo.Mono.Dtos.Location;
using FluentValidation;

namespace DedicatedGeo.Mono.Core.Location;

public class PostLocationPointsS2DRequestValidator: AbstractValidator<PostLocationPointsS2DRequest>
{
    public PostLocationPointsS2DRequestValidator()
    {
        RuleFor(x => x.DeviceId).NotNull().NotEmpty().MaximumLength(16);
        RuleFor(x => x.LocationPoints).NotNull().NotEmpty();
        RuleForEach(x => x.LocationPoints)
            .ChildRules(locationPoint =>
            {
                locationPoint.RuleFor(x => x.Latitude)
                    .InclusiveBetween(-90, 90)
                    .WithMessage("Latitude must be between -90 and 90.");
                
                locationPoint.RuleFor(x => x.Longitude)
                    .InclusiveBetween(-180, 180)
                    .WithMessage("Longitude must be between -180 and 180.");
            });
    }
}