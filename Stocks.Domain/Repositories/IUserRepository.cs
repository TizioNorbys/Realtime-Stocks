namespace Stocks.Domain.Repositories;

public interface IUserRepository
{
    Task<bool> IsEmailUniqueAsync(string email);
}