namespace Stocks.Infrastracture.SignalR;

public record StockPriceUpdate(string Symbol, decimal Price);