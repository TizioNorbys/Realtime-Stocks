using Stocks.Domain.Entities;

namespace Stocks.Application.Abstractions.Authentication;

public interface IJwtProvider
{
    string Generate(AppUser user);
}