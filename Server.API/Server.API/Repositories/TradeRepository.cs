using Microsoft.EntityFrameworkCore;
using Server.API.Models;
using Server.API.Repositories;
using Server.API.Utils;

public class TradeRepository : ITradeRepository
{
    private readonly GamesContext context;

    public TradeRepository(GamesContext context)
    {
        this.context = context;
    }

    public async Task<List<Trade>> GetTradesAsync()
    {
        return await context.Trades.ToListAsync();
    }

    public async Task<Trade> GetTradeByIdAsync(Guid id)
    {
        var trade = await context.Trades.FindAsync(id);

        if (trade == null)
        {
            throw new KeyNotFoundException("Trade not found");
        }

        return trade;
    }

    public async Task AddTradeAsync(Trade trade)
    {
        context.Trades.Add(trade);
        await context.SaveChangesAsync();
    }

    public async Task DeleteTradeAsync(Guid id)
    {
        var trade = context.Trades.Find(id);
        if (trade == null)
        {
            throw new KeyNotFoundException("Trade not found");
        }
        context.Trades.Remove(trade);
        await context.SaveChangesAsync();
    }

    public async Task UpdateTradeAsync(Guid id, Trade trade)
    {
        if (context.Trades.Find(id) == null)
        {
            throw new KeyNotFoundException("Trade not found");
        }
        context.Trades.Update(trade);
        await context.SaveChangesAsync();
    }
}
