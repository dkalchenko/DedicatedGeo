using FluentValidation;

namespace DedicatedGeo.Mono.Core.Extensions;

public static class ValidatorExtensions
{
    public static IRuleBuilderOptions<T, string> MustBeGuid<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.NotEmpty().NotNull().Must(x => Guid.TryParse(x, out _));
    }
    public static IRuleBuilderOptions<T, string> MustBeListGuids<T>(this IRuleBuilder<T, string> ruleBuilder, int min, int max)
    {
        return ruleBuilder.NotEmpty().NotNull().Must( ids =>
        {
            var categoryIds = ids!.Split(',');
            return categoryIds.All(categoryId => Guid.TryParse(categoryId, out _)) &&
                   categoryIds.Length >= min && categoryIds.Length <= max;
        });
    }
    
}