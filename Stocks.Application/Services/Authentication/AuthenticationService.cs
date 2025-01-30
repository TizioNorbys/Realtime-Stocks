using FluentResults;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Stocks.Application.Abstractions.Authentication;
using Stocks.Application.DTOs.Authentication;
using Stocks.Application.Errors;
using Stocks.Application.Services.Interfaces;
using Stocks.Domain.Entities;

namespace Stocks.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
	private readonly IJwtProvider _jwtProvider;
	private readonly IValidator<RegistrationDto> _registrationValidator;
	private readonly IValidator<LoginDto> _loginValidator;
	private readonly ILogger<AuthenticationService> _logger;
	private readonly UserManager<AppUser> _userManager;

    public AuthenticationService(
        IJwtProvider jwtProvider,
        IValidator<RegistrationDto> registrationValidator,
        IValidator<LoginDto> loginValidator,
        ILogger<AuthenticationService> logger,
        UserManager<AppUser> userManager)
	{
		_jwtProvider = jwtProvider;
		_registrationValidator = registrationValidator;
		_loginValidator = loginValidator;
		_logger = logger;
		_userManager = userManager;
	}

	public async Task<Result> SignUp(RegistrationDto request)
	{
		var valResult = await _registrationValidator.ValidateAsync(request);
		if (!valResult.IsValid)
		{
			_logger.LogWarning("Registration request validation failed. {errors}", valResult.ToDictionary());
            return Result.Fail(AppErrors.Validation(valResult.Errors));
        }

		AppUser user = new(request.Email, request.FirstName, request.LastName);
	
		var identityResult = await _userManager.CreateAsync(user, request.Password);
		if (!identityResult.Succeeded)
		{
			var errors = identityResult.Errors.ToDictionary(e => e.Code, e => e.Description);
			_logger.LogWarning("Error while creating {userId} user. {errors}", user.Id, errors);

            return Result.Fail(AppErrors.Registration(identityResult.Errors));
        }

        _logger.LogInformation("{userId} user successfully signed in", user.Id);

		return Result.Ok();
	}

	public async Task<Result<string>> SignIn(LoginDto request)
	{
		var valResult = _loginValidator.Validate(request);
		if (!valResult.IsValid)
		{
            _logger.LogWarning("Login request validation failed. {errors}", valResult.ToDictionary());
            return Result.Fail(AppErrors.Validation(valResult.Errors));
        }
			
		var user = await _userManager.FindByEmailAsync(request.Email);
		if (user is null)
		{
			_logger.LogWarning("User not found");
            return Result.Fail(AppErrors.InvalidCredentials);
        }
			
		_logger.LogDebug("Checking password for {userId} user", user.Id);

		if (!await _userManager.CheckPasswordAsync(user, request.Password))
		{
			_logger.LogWarning("Incorrect password for {userId} user", user.Id);
            return Result.Fail(AppErrors.InvalidCredentials);
        }
			
		_logger.LogInformation("{userId} user logged in", user.Id);

		_logger.LogDebug("Generating authentication token for {userId} user", user.Id);
		var token = _jwtProvider.Generate(user);

		return Result.Ok(token);
	}
}