using Microsoft.EntityFrameworkCore;
using Stocks.Domain.Repositories;

namespace Stocks.Infrastracture.Persistence.Repositories;

public class UserRepository : IUserRepository
{
	private readonly AppDbContext _dbContext;

	public UserRepository(AppDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task<bool> IsEmailUniqueAsync(string email)
	{
        return !await _dbContext.Users.AnyAsync(u => u.Email == email);
	}
}