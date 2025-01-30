namespace Stocks.Application.DTOs.Base;

public interface IFinnHubData
{
    public decimal CurrentPrice { get; set; }

    public decimal? Change { get; set; }

    public decimal? PercentChange { get; set; }

    public decimal High { get; set; }

    public decimal Low { get; set; }

    public decimal Open { get; set; }

    public decimal PreviousClose { get; set; }

    public long UnixTimeStamp { get; set; }
}