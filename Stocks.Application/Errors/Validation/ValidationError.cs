using FluentResults;
using FluentValidation.Results;

namespace Stocks.Application.Errors.Validation;

public class ValidationError : Error
{
    public ValidationError(IEnumerable<ValidationFailure> errors)
        : base("Validation error")
    {
        var metadata = errors.ToDictionary(e => e.PropertyName, e => (object)e.ErrorMessage);
        WithMetadata(metadata);
    }
}