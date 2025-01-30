using System.Text.Json.Serialization;
using Stocks.Application.DTOs.Base;

namespace Stocks.Application.DTOs.Stocks;

public class StocksDataDto : IFinnHubData
{
    [JsonPropertyName("c")]
    public decimal CurrentPrice { get; set; }

    [JsonPropertyName("d")]
    public decimal? Change { get; set; }

    [JsonPropertyName("dp")]
    public decimal? PercentChange { get; set; }

    [JsonPropertyName("h")]
    public decimal High { get; set; }

    [JsonPropertyName("l")]
    public decimal Low { get; set; }

    [JsonPropertyName("o")]
    public decimal Open { get; set; }

    [JsonPropertyName("pc")]
    public decimal PreviousClose { get; set; }

    [JsonPropertyName("t")]
    public long UnixTimeStamp { get; set; }
}