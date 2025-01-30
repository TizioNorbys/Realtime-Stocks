namespace Stocks.API.Models.Responses.Account;

public record LoginResponse(string Message, string? Token)
{
    public static LoginResponse Success(string token) => new("Successfully logged in", token);

    public static LoginResponse Fail(string message) => new(message, null);
}