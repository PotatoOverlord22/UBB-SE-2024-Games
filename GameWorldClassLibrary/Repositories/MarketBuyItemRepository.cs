using GameWorldClassLibrary.Models;
using GameWorldClassLibrary.Utils;
using Microsoft.EntityFrameworkCore;

namespace GameWorldClassLibrary.Repositories
{
    public class MarketBuyItemRepository : IMarketBuyItemRepository
    {
        private readonly GamesContext context;

        public MarketBuyItemRepository(GamesContext context)
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
