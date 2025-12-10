using DedicatedGeo.Mono.Dtos.Location;
using FluentValidation;

namespace DedicatedGeo.Mono.Core.Location;

public class PostLocationPointsPublicRequestValidator: AbstractValidator<PostLocationPointsPublicRequest>
{
    public PostLocationPointsPublicRequestValidator()
    {
        RuleFor(x => x.LocationPoints).NotNull().NotEmpty();
    }
}