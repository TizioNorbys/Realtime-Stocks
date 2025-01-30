namespace Stocks.Application.DTOs.Authentication;

public record RegistrationDto(
    string Email,
    string FirstName,
    string LastName,
    string Password,
    string ConfirmPassword);