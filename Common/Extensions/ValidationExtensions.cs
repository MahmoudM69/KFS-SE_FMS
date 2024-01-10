using Common.Interfaces;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Extensions;

public static class ValidationExtension
{
    public static void HandelNullableEntity<Entity, Property>(this (Property? property, ValidationContext<Entity> context) action,
                                                              AbstractValidator<Property> validator, bool isNullable = true)
        where Entity : class, IValidatable, new()
        where Property : class, IValidatable, new()
    {
        if (!isNullable || action.property != null)
        {
            var result = validator.Validate(action.property!);
            if (!result.IsValid)
            {
                result.Errors.Iter(action.context.AddFailure);
            }
        }
        else if (isNullable)
        {
            action.context.AddFailure($"The {typeof(Property).Name} cannot be null.");
        }
    }
    public async static Task HandelNullableEntityAsync<Entity, Property>(this (Property? property, ValidationContext<Entity> context) action,
                                                                               AbstractValidator<Property> validator, bool isNullable = true)
        where Entity : IValidatable, new()
        where Property : IValidatable, new()
    {
        if (!isNullable || action.property != null)
        {
            var result = await validator.ValidateAsync(action.property!);
            if (!result.IsValid)
            {
                result.Errors.Iter(action.context.AddFailure);
            }
        }
        else if (isNullable)
        {
            action.context.AddFailure($"The {typeof(Property).Name} cannot be null.");
        }
    }
    public async static Task HandelNullableEntityAsync<Entity, Property>(this (Property? property, ValidationContext<Entity> context, CancellationToken cancellationToken) action,
                                                                               AbstractValidator<Property> validator, bool isNullable = true)
        where Entity : IValidatable, new()
        where Property : IValidatable, new()
    {
        if (!isNullable || action.property != null)
        {
            var result = await validator.ValidateAsync(action.property!, action.cancellationToken);
            if (!result.IsValid)
            {
                result.Errors.Iter(action.context.AddFailure);
            }
        }
        else if (isNullable)
        {
            action.context.AddFailure($"The {typeof(Property).Name} cannot be null.");
        }
    }
}
