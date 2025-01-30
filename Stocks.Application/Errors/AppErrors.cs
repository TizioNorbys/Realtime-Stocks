using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Stocks.Application.Errors.Authentication;
using Stocks.Application.Errors.Stocks;
using Stocks.Application.Errors.Validation;

namespace Stocks.Application.Errors;

public static class AppErrors
{
    public static InvalidCredentialsError InvalidCredentials => new();

    public static InvalidSymbolError InvalidSymbol(string symbol) => new(symbol);

    public static HistoricalSearchError HistoricalSearch(string symbol, DateOnly startDate, DateOnly endDate)
        => new(symbol, startDate, endDate);

    public static ValidationError Validation(IEnumerable<ValidationFailure> errors) => new(errors);

    public static RegistrationError Registration(IEnumerable<IdentityError> errors) => new(errors);
}