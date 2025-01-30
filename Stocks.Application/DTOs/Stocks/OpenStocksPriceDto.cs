using Stocks.Application.DTOs.Stocks.Base;

namespace Stocks.Application.DTOs.Stocks;

public record OpenStocksPriceDto : IStockPriceDto
{
    public string Symbol { get; init; }

    public decimal CurrentPrice { get; init; }

    public OpenStocksPriceDto(string symbol, decimal currentPrice)
    {
        Symbol = symbol;
        CurrentPrice = Math.Round(currentPrice, 2);
    }
}