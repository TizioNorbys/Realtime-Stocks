namespace Stocks.API.Models.Requests.Stocks;

public class HistoricalDataRequest
{
    public DateOnly StartDate { get; init; }

    public DateOnly EndDate { get; init; }
}