using FluentValidation;
using DomainValidationException = GymApp.Domain.Exceptions.ValidationException;

namespace GymApp.Application.Common.Extensions;

public static class ValidationExtensions
{
    public static async Task ValidateAndThrowDomainAsync<T>(
        this IValidator<T>? validator, 
        T instance, 
        CancellationToken ct = default)
    {
        if (validator is null) return;

        var result = await validator.ValidateAsync(instance, ct);

        if (!result.IsValid)
        {
            var errors = result.Errors
                .GroupBy(x => x.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(x => x.ErrorMessage).ToArray());

            throw new DomainValidationException(errors);
        }
    }
}