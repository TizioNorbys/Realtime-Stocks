using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Stocks.Application.Services.Interfaces;
using Stocks.Application.Services.Stocks;
using Stocks.Infrastracture.SignalR;

namespace Stocks.Infrastracture.BackgroundServices;

public class StocksPriceUpdater : BackgroundService
{
    private readonly IHubContext<StocksHub, IStocksHubClient> _hubContext;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<StocksPriceUpdater> _logger;
    private readonly SymbolsManager _symbolsManager;

    public StocksPriceUpdater(
        IHubContext<StocksHub, IStocksHubClient> hubContext,
        IServiceScopeFactory serviceScopeFactory,
        ILogger<StocksPriceUpdater> logger,
        SymbolsManager symbolsManager)
    {
        _hubContext = hubContext;
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
        _symbolsManager = symbolsManager;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await UpdatePrice();

            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }

    private async Task UpdatePrice()
    {
        using IServiceScope scope = _serviceScopeFactory.CreateScope();
        var stocksService = scope.ServiceProvider.GetRequiredService<IStocksService>();

        foreach (string symbol in _symbolsManager.GetActiveSymbols())
        {
            var result = await stocksService.GetCurrentPrice(symbol);
            var stockData = result.Value;

            StockPriceUpdate updatedPrice = new(symbol, stockData.CurrentPrice);

            _logger.LogInformation("Broadcasting updated price for {symbol} symbol", symbol);
            await _hubContext.Clients.Group(symbol).ReceiveUpdatedPrice(updatedPrice);
        }
    }
}