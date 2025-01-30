using System.Text.Json;
using Microsoft.Extensions.Options;
using Stocks.Application.Abstractions.Clients;
using Stocks.Application.DTOs.Stocks;

namespace Stocks.Infrastracture.Clients.Stocks;

public class StocksClient : IStocksClient
{
    private readonly HttpClient _client;
    private readonly StocksClientOptions _options;

    public StocksClient(HttpClient client, IOptions<StocksClientOptions> options)
    {
        _client = client;
        _options = options.Value;
    }

    public async Task<StocksDataDto?> GetStockData(string symbol)
    {
        string json = await _client.GetStringAsync($"quote?symbol={symbol}&token={_options.ApiKey}");
        var stocksData = JsonSerializer.Deserialize<FinnHubData>(json);

        if (stocksData is null)
            return null;

        var stocksDataDto = new StocksDataDto();
        stocksData.MapTo(stocksDataDto);
        return stocksDataDto;
    }
}