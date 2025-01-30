using Microsoft.AspNetCore.Mvc;
using Stocks.API.Models.Requests.Account;
using Stocks.API.Models.Responses.Account;
using Stocks.Application.DTOs.Authentication;
using Stocks.Application.Errors.Authentication;
using Stocks.Application.Errors.Validation;
using Stocks.Application.Services.Interfaces;
using StocksApi.Extensions;

namespace Chat.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
	private readonly IAuthenticationService _authenticationService;
	private readonly ILogger<AccountController> _logger;

	public AccountController(IAuthenticationService authenticationService, ILogger<AccountController> logger)
	{
		_authenticationService = authenticationService;
		_logger = logger;
	}

	[HttpPost("[action]")]
	public async Task<IActionResult> Registration([FromBody] RegistrationRequest request)
	{
        RegistrationDto registrationDto = new
			(request.Email, request.FirstName, request.LastName, request.Password, request.ConfirmPassword);

		var signUpResult = await _authenticationService.SignUp(registrationDto);
		if (signUpResult.IsFailed)
        {
			return signUpResult.GetFirstError() switch
			{
				ValidationError err => ValidationProblem(ModelState.AddValidationErrors(err.Metadata)),
				RegistrationError err => BadRequest(err.Metadata)
			};		
		}

		return Created();
	}

	[HttpPost("[action]")]
	public async Task<IActionResult> Login([FromBody] LoginRequest request)
	{
		LoginDto loginDto = new(request.Email, request.Password);

		var loginResult = await _authenticationService.SignIn(loginDto);
		if (loginResult.IsFailed)
		{
			return loginResult.GetFirstError() switch
			{
				ValidationError err => ValidationProblem(ModelState.AddValidationErrors(err.Metadata)),
				InvalidCredentialsError err => NotFound(LoginResponse.Fail(err.Message))
			};
		}

		var token = loginResult.Value;
		return Ok(LoginResponse.Success(token));
	}
}