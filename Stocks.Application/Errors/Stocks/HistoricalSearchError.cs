using FluentResults;

namespace Stocks.Application.Errors.Stocks;

public class HistoricalSearchError : Error
{
    public HistoricalSearchError(string symbol, DateOnly startDate, DateOnly endDate)
        : base($"There are no historical prices of \"{symbol}\" between {startDate} and {endDate}")
    {
    }
}