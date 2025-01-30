using FluentResults;
using Microsoft.AspNetCore.Identity;

namespace Stocks.Application.Errors.Authentication;

public class RegistrationError : Error
{
    public RegistrationError(IEnumerable<IdentityError> errors)
        : base("Error during registration")
    {
        var metadata = errors.ToDictionary(e => e.Code, e => (object)e.Description);
        WithMetadata(metadata);
    }
}