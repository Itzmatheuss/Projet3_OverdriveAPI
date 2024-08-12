using FluentValidation.Results;

namespace Projeto3_Over.Models.Error
{
    public static class ValidationFailureExtension
    {
        public static IList<CustomValidationFailure> ToCustomValidationFailure(this IList<ValidationFailure> failures)
        {
            return failures.Select(failure => new CustomValidationFailure(failure.PropertyName, failure.ErrorMessage)).ToList();
        }
    }
}
