using Stocks.Application.DTOs.Stocks;

namespace Stocks.Application.Abstractions.Clients;

public interface IStocksClient
{
    Task<StocksDataDto?> GetStockData(string symbol);
}