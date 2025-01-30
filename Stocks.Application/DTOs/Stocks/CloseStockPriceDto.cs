using Stocks.Application.DTOs.Stocks.Base;

namespace Stocks.Application.DTOs.Stocks;

public record CloseStockPriceDto : IStockPriceDto
{
    public string Symbol { get; init; }

    public decimal CurrentPrice { get; init; }

    public decimal ClosingPrice { get; init; }

    public CloseStockPriceDto(string symbol, decimal currentPrice, decimal closingPrice)
    {
        Symbol = symbol;
        CurrentPrice = Math.Round(currentPrice, 2);
        ClosingPrice = Math.Round(closingPrice, 2);
    }
}