using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Stocks.Application.Abstractions.Authentication;
using Stocks.Application.Abstractions.Clients;
using Stocks.Domain.Entities;
using Stocks.Domain.Repositories;
using Stocks.Infrastracture.Authentication;
using Stocks.Infrastracture.BackgroundServices;
using Stocks.Infrastracture.Clients.Stocks;
using Stocks.Infrastracture.Persistence;
using Stocks.Infrastracture.Persistence.Repositories;
using Stocks.Infrastracture.SignalR;

namespace Stocks.Infrastracture;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastracture(this IServiceCollection services)
	{
		services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IStockRepository, StockRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddDbContext<AppDbContext>((provider, dbOptions) =>
        {
            var options = provider.GetRequiredService<IOptions<ConnectionStringsOptions>>().Value;
            dbOptions.UseMySQL(connectionString: options.Default);   
        });

        services.AddIdentity<AppUser, AppRole>(options =>
        {
            options.User.RequireUniqueEmail = true;
            options.Lockout.MaxFailedAccessAttempts = 5;

            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireDigit = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequiredLength = 10;
        })
        .AddEntityFrameworkStores<AppDbContext>()
        .AddRoles<AppRole>();

        services.AddHttpClient<IStocksClient, StocksClient>((provider, client) =>
        {
            var options = provider.GetRequiredService<IOptions<StocksClientOptions>>().Value;
            client.BaseAddress = new Uri(options.BaseAddress);
        });

        services.AddHostedService<StocksPriceUpdater>();

        services.AddMemoryCache();

        services.AddSignalR();
        services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();

        return services;
	}
}