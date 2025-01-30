using Stocks.Domain.Common;
using Stocks.Domain.Exceptions;

namespace Stocks.Domain.Entities;

public class Stock : Entity
{
    public string Symbol { get; private set; } = string.Empty;

    public decimal Price { get; private set; }

    public DateTime DateTime { get; private set; }

    public Stock() { }

    public Stock(string symbol, decimal price, DateTime dateTime)
        : base()
    {
        Symbol = !string.IsNullOrWhiteSpace(symbol) ? symbol : throw new ArgumentException("Symbol cannot be null or empty");
        Price = price > 0 ? Math.Round(price, 2) : throw new NegativePriceException();
        DateTime = dateTime;
    }
}