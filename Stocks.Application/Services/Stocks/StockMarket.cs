namespace Stocks.Application.Services.Stocks;

public class StockMarket
{
    private static readonly TimeSpan openTime = new(14, 30, 0);
    private static readonly TimeSpan closeTime = new(21, 0, 0);

    public StockMarket()
    {
        SetMarketStatus();
    }

    public bool IsOpen { get; private set; }

    public bool IsClosed => !IsOpen;

    private void SetMarketStatus()
    {
        var currentTime = DateTime.UtcNow.TimeOfDay;
        IsOpen = currentTime >= openTime && currentTime <= closeTime;
    }
}