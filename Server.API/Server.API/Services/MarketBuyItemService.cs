using Microsoft.EntityFrameworkCore;
using Server.API.Models;
using Server.API.Utils;

namespace Server.API.Services
{
    public class MarketBuyItemService : IMarketBuyItemService
    {
        private readonly GamesContext context;

        public MarketBuyItemService(GamesContext context)
        {
            this.context = context;
        }

        public async Task AddMarketBuyItemAsync(MarketBuyItem marketBuyItem)
        {
            context.MarketBuyItems.Add(marketBuyItem);
            await context.SaveChangesAsync();
        }

        public async Task DeleteMarketBuyItemAsync(Guid marketBuyItemId)
        {
            var marketBuyItem = context.MarketBuyItems.Find(marketBuyItemId) ?? throw new KeyNotFoundException("Market buy item not found");
            context.MarketBuyItems.Remove(marketBuyItem);
            await context.SaveChangesAsync();
        }

        public async Task<List<MarketBuyItem>> GetAllMarketBuyItemsAsync()
        {
            return await context.MarketBuyItems.ToListAsync();
        }

        public async Task<MarketBuyItem> GetMarketBuyItemByItemIdAsync(Guid itemId)
        {
            var marketBuyItem = await context.MarketBuyItems.FindAsync(itemId) ?? throw new KeyNotFoundException("Market buy item not found");
            return marketBuyItem;
        }

        public async Task UpdateMarketBuyItemAsync(MarketBuyItem marketBuyItem)
        {
            if (context.MarketBuyItems.Find(marketBuyItem.Id) == null)
            {
                throw new KeyNotFoundException("Market buy item not found");
            }

            context.MarketBuyItems.Update(marketBuyItem);
            await context.SaveChangesAsync();
        }
    }
}
