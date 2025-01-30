using FluentResults;
using Stocks.Application.DTOs.Stocks;
using Stocks.Application.DTOs.Stocks.Base;

namespace Stocks.Application.Services.Interfaces;

public interface IStocksService
{
    Task<Result<IStockPriceDto>> GetCurrentPrice(string symbol);

    Task SaveData(string symbol, StocksDataDto stockData);

    Task<Result<Dictionary<DateTime, decimal>>> GetHistoricalData(string symbol, HistoricalDataDto request);
}