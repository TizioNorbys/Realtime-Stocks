using System.Text.Json.Serialization;
using Stocks.Application.DTOs.Base;

namespace Stocks.Infrastracture.Clients.Stocks;

public record FinnHubData 
{
    [JsonPropertyName("c")]
    public decimal CurrentPrice { get; init; }

    [JsonPropertyName("d")]
    public decimal? Change { get; init; }

    [JsonPropertyName("dp")]
    public decimal? PercentChange { get; init; }

    [JsonPropertyName("h")]
    public decimal High { get; init; }

    [JsonPropertyName("l")]
    public decimal Low { get; init; }

    [JsonPropertyName("o")]
    public decimal Open { get; init; }

    [JsonPropertyName("pc")]
    public decimal PreviousClose { get; init; }

    [JsonPropertyName("t")]
    public long UnixTimeStamp { get; init; }

    public void MapTo<T>(T value) where T : IFinnHubData
    {
        value.CurrentPrice = CurrentPrice;
        value.Change = Change;
        value.PercentChange = PercentChange;
        value.High = High;
        value.Low = Low;
        value.Open = Open;
        value.PreviousClose = PreviousClose;
        value.UnixTimeStamp = UnixTimeStamp;
    }
}