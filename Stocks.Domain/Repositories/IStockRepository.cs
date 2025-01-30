using Stocks.Domain.Entities;

namespace Stocks.Domain.Repositories;

public interface IStockRepository
{
    void Add(Stock stock);

    Task<Dictionary<DateTime, decimal>> GetDataBetween(string symbol, DateTime startDate, DateTime endDate);

    Task SaveChangesAsync();
}