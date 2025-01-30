namespace Stocks.API.Models.Requests.Account
{
    public record RegistrationRequest(
        string Email,
        string FirstName,
        string LastName,
        string Password,
        string ConfirmPassword);
}