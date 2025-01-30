using FluentValidation;
using Stocks.Application.DTOs.Authentication;

namespace Stocks.Application.Validators.Authentication;

public class LoginValidator : AbstractValidator<LoginDto>
{
    public LoginValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(256);

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(10);
    }
}