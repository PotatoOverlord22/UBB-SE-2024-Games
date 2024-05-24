using Microsoft.EntityFrameworkCore;
using Server.API.Models;
using Server.API.Utils;

namespace Server.API.Services
{
    public class MarketSellResourceService : IMarketSellResourceService
    {
        private readonly GamesContext context;

        public MarketSellResourceService(GamesContext context)
        {
            this.context = context;
        }

        public async Task GetAllMarketSellResourcesAsync()
        {
            return await context.MarketSellResources.ToListAsync();
        }

        public async Task GetMarketSellResourceByResourceIdAsync(Guid resourceId)
        {
            var marketSellItem = await context.MarketSellItems.FindAsync(resourceId) ?? throw new KeyNotFoundException("Market buy item not found");
            return marketSellItem;
        }

        public async Task AddMarketSellResourceAsync(IMarketSellResource marketSellResource)
        {
            context.MarketSellResources.Add(marketSellResource);
            await context.SaveChangesAsync();
        }

        public async Task UpdateMarketSellResourceAsync(MarketSellResource marketSellResource)
        {
            if (context.MarketSellResources.Find(marketSellResource.Id) == null)
            {
                throw new KeyNotFoundException("Market sell resource not found");
            }
            context.MarketSellResources.Update(marketSellResource);
            await context.SaveChangesAsync();
        }

        public async Task DeleteMarketSellResourceAsync(Guid marketSellResourceId)
        {
            var marketSellResource = context.MarketSellResources.Find(marketSellResourceId) ?? throw new KeyNotFoundException("Market sell resource not found");
            context.MarketSellResources.Remove(marketSellResource);
            await context.SaveChangesAsync();
        }
    }
}
