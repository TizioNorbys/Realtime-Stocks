using FluentResults;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Stocks.Application.Abstractions.Clients;
using Stocks.Application.DTOs.Stocks;
using Stocks.Application.DTOs.Stocks.Base;
using Stocks.Application.Errors;
using Stocks.Application.Services.Interfaces;
using Stocks.Domain.Entities;
using Stocks.Domain.Repositories;

namespace Stocks.Application.Services.Stocks;

public class StocksService : IStocksService
{
    private readonly IStocksClient _stocksClient;
    private readonly IStockRepository _stockRepository;
    private readonly IValidator<HistoricalDataDto> _historicalDataValidator;
    private readonly ILogger<StocksService> _logger;
    private readonly SymbolsManager _symbolsManager;
    private readonly StockMarket _stockMarket;

    public StocksService(
        IStocksClient stocksClient,
        IStockRepository stockRepository,
        IValidator<HistoricalDataDto> historicalDataValidator,
        ILogger<StocksService> logger,
        SymbolsManager symbolsManager,
        StockMarket stockMarket)
    {
        _stocksClient = stocksClient;
        _symbolsManager = symbolsManager;
        _stockRepository = stockRepository;
        _historicalDataValidator = historicalDataValidator;
        _stockMarket = stockMarket;
        _logger = logger;
    }

    public async Task<Result<IStockPriceDto>> GetCurrentPrice(string symbol)
    {
        _logger.LogInformation("Requested price for {symbol} symbol", symbol);

        var stockData = await _stocksClient.GetStockData(symbol);
        if (stockData is null || !IsValidSymbol(stockData))
        {
            _logger.LogWarning("{symbol} symbol is not valid", symbol);
            return Result.Fail(AppErrors.InvalidSymbol(symbol));
        }

        _symbolsManager.AddSymbol(symbol);
        
        if (_stockMarket.IsOpen)
        {
            await SaveData(symbol, stockData);

            OpenStocksPriceDto openStockPrice = new(symbol, stockData.CurrentPrice);
            return Result.Ok<IStockPriceDto>(openStockPrice);
        }

        CloseStockPriceDto closeStockPrice = new(symbol, stockData.CurrentPrice, stockData.PreviousClose);
        return Result.Ok<IStockPriceDto>(closeStockPrice);
    }

    public async Task SaveData(string symbol, StocksDataDto stockData)
    {
        var stockDateTime = DateTimeOffset.FromUnixTimeSeconds(stockData.UnixTimeStamp).DateTime;

        Stock stock = new(symbol, stockData.CurrentPrice, stockDateTime);

        _stockRepository.Add(stock);
        await _stockRepository.SaveChangesAsync();
    }

    public async Task<Result<Dictionary<DateTime, decimal>>> GetHistoricalData(string symbol, HistoricalDataDto request)
    {
        var valResult = await _historicalDataValidator.ValidateAsync(request);

        if (!valResult.IsValid)
        {
            _logger.LogWarning("Historical data request validation failed. {errors}", valResult.ToDictionary());
            return Result.Fail(AppErrors.Validation(valResult.Errors));
        }
            
        DateTime start = request.StartDate.ToDateTime(TimeOnly.MinValue);
        DateTime end = request.EndDate.ToDateTime(TimeOnly.MaxValue);

        var data = await _stockRepository.GetDataBetween(symbol, start, end);
        if (data.Count == 0)
        {
            _logger.LogWarning("No data for {symbol} symbol between {startDate} and {endDate}", symbol, start, end);
            return Result.Fail(AppErrors.HistoricalSearch(symbol, request.StartDate, request.EndDate));
        }
        
        var sortedValues = data.OrderByDescending(x => x.Key).ToDictionary();
        return Result.Ok(sortedValues);
    }

    private static bool IsValidSymbol(StocksDataDto stockData)
    {
        return stockData.Change != null && stockData.PercentChange != null;
    }
}