namespace Stocks.Domain.Exceptions;

public class NegativePriceException : Exception
{
    public NegativePriceException()
        : base("Stock price cannot be lower than 0")
    {
    }
}