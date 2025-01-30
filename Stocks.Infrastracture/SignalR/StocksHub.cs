using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Stocks.Application.Services.Stocks;

namespace Stocks.Infrastracture.SignalR;

[Authorize]
public class StocksHub : Hub<IStocksHubClient>
{
    public override async Task OnConnectedAsync()
    {
        foreach (string symbol in SymbolsManager.GetDefaultSymbols())
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, symbol);
        }
    }

    public async Task AddToGroupAsync(string symbol)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, symbol.ToUpper());
    }
}