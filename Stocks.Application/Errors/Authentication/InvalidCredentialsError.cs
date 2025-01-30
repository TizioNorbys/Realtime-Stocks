using FluentResults;

namespace Stocks.Application.Errors.Authentication;

public class InvalidCredentialsError : Error
{
	public InvalidCredentialsError()
		: base("Invalid email or password")
	{
	}
}