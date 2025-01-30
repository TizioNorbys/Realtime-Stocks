using Microsoft.EntityFrameworkCore;
using Stocks.Application.DTOs.Stocks;
using Stocks.Domain.Entities;
using Stocks.Domain.Repositories;

namespace Stocks.Infrastracture.Persistence.Repositories;

public class StockRepository : IStockRepository
{
    private readonly AppDbContext _dbContext;

    public StockRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(Stock stock)
    {
        _dbContext.Stocks.Add(stock);
    }

    public async Task<Dictionary<DateTime, decimal>> GetDataBetween(string symbol, DateTime startDate, DateTime endDate)
    {
        return await (from stock in _dbContext.Stocks
                      where stock.Symbol == symbol
                      let date = stock.DateTime
                      where date >= startDate && date <= endDate
                      select new StocksDetails(stock.DateTime, stock.Price)
                ).ToDictionaryAsync(x => x.DateTime, x => x.Price);
    }

    public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();
}