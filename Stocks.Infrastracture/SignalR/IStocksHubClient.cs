namespace Stocks.Infrastracture.SignalR;

public interface IStocksHubClient
{
    Task ReceiveUpdatedPrice(StockPriceUpdate updatedPrice);
}