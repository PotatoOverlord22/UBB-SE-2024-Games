using Microsoft.EntityFrameworkCore;
using Server.API.Models;
using Server.API.Utils;

namespace Server.API.Repositories
{
    public class MarketSellResourceRepository : IMarketSellResourceRepository
    {
        private readonly GamesContext context;

        public MarketSellResourceRepository(GamesContext context)
        {
            this.context = context;
        }

        public async Task<List<MarketSellResource>> GetAllMarketSellResourcesAsync()
        {
            return await context.MarketSellResources.ToListAsync();
        }

        public async Task<MarketSellResource> GetMarketSellResourceByResourceIdAsync(Guid resourceId)
        {
            var marketSellItem = await context.MarketSellResources.FindAsync(resourceId) ?? throw new KeyNotFoundException("Market sell item not found");
            return marketSellItem;
        }

        public async Task AddMarketSellResourceAsync(MarketSellResource marketSellResource)
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
