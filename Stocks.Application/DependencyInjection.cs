using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Stocks.Application.Services.Authentication;
using Stocks.Application.Services.Interfaces;
using Stocks.Application.Services.Stocks;

namespace Stocks.Application;

public static class DependencyInjection
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
        services.AddScoped<IStocksService, StocksService>();
		services.AddScoped<IAuthenticationService, AuthenticationService>();
		services.AddScoped<StockMarket>();
		services.AddSingleton<SymbolsManager>();

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

		return services;
	}
}