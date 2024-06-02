using Microsoft.EntityFrameworkCore;
using GameWorldClassLibrary.Models;
using GameWorldClassLibrary.Utils;

namespace GameWorldClassLibrary.Repositories
{
    public class MarketSellResourceRepositoryDB : IMarketSellResourceRepository
    {
        private readonly GamesContext context;

        public MarketSellResourceRepositoryDB(GamesContext context)
        {
            this.context = context;
        }

        public async Task<List<MarketSellResource>> GetAllSellResourcesAsync()
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
