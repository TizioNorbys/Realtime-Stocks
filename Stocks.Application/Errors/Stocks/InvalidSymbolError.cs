using FluentResults;

namespace Stocks.Application.Errors.Stocks;

public class InvalidSymbolError : Error
{
    public InvalidSymbolError(string symbol)
        : base($"\"{symbol}\" is not a valid symbol")
    {
    }
}