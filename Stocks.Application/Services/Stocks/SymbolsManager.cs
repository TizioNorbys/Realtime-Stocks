namespace Stocks.Application.Services.Stocks;

public class SymbolsManager
{
    private static readonly IReadOnlySet<string> defaultSymbols = new HashSet<string> { "MSFT", "AAPL", "NVDA" };
    private readonly HashSet<string> activeSymbols = new(defaultSymbols);

    public void AddSymbol(string symbol)
    {
        activeSymbols.Add(symbol);
    }

    public static IReadOnlyCollection<string> GetDefaultSymbols()
    {
        return defaultSymbols.ToHashSet();
    }

    public IReadOnlyCollection<string> GetActiveSymbols()
    {
        return activeSymbols.ToHashSet();
    }
}