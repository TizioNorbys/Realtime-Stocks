using FluentResults;
using Stocks.Application.DTOs.Authentication;

namespace Stocks.Application.Services.Interfaces;

public interface IAuthenticationService
{
    Task<Result> SignUp(RegistrationDto request);

    Task<Result<string>> SignIn(LoginDto request);
}