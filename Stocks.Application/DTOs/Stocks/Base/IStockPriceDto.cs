namespace Stocks.Application.DTOs.Stocks.Base;

public interface IStockPriceDto
{
    string Symbol { get; init; }

    decimal CurrentPrice { get; init; }
}