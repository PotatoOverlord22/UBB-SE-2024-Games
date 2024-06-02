using Microsoft.EntityFrameworkCore;
using GameWorldClassLibrary.Models;
using GameWorldClassLibrary.Utils;

namespace GameWorldClassLibrary.Repositories
{
    public class TradeRepositoryDB : ITradeRepository
    {
        private readonly GamesContext context;

        public TradeRepositoryDB(GamesContext context)
        {
            this.context = context;
        }

        public async Task<List<Trade>> GetAllTradesAsync()
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

        public async Task CreateTradeAsync(Trade trade)
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

        public async Task UpdateTradeAsync(Trade trade)
        {
            if (context.Trades.Find(trade.Id) == null)
            {
                throw new KeyNotFoundException("Trade not found");
            }
            context.Trades.Update(trade);
            await context.SaveChangesAsync();
        }

        public async Task<List<Trade>> GetAllTradesExceptCreatedByUser(Guid userId)
        {
            return await context.Trades
                .Where(trade => trade.User == null || trade.User.Id != userId)
                .ToListAsync();
        }

        public async Task<Trade> GetUserTradeAsync(Guid userId)
        {
            var trade = await context.Trades
                .FirstOrDefaultAsync(trade => trade.User != null && trade.User.Id == userId);

            if (trade == null)
            {
                throw new KeyNotFoundException("Trade not found for the user");
            }

            return trade;
        }
    }
}
