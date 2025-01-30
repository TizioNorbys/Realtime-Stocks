using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Stocks.Application.DTOs.Authentication;
using Stocks.Domain.Entities;
using Stocks.Domain.Repositories;

namespace Stocks.Application.Validators.Authentication;

public class RegistrationValidator : AbstractValidator<RegistrationDto>
{
    public RegistrationValidator(IUserRepository userRepository)
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(256)
            .MustAsync(async (email, _) =>
                await userRepository.IsEmailUniqueAsync(email))
                .WithMessage("Email is already in use");

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8);

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password)
                .WithMessage("Passwords don't match");
    }
}