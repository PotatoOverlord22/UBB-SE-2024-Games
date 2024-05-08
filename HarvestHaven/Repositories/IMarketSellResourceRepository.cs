using HarvestHaven.Entities;

namespace HarvestHaven.Repositories
{
    public interface IMarketSellResourceRepository
    {
        Task<List<MarketSellResource>> GetAllSellResourcesAsync();
        Task<MarketSellResource> GetMarketSellResourceByResourceIdAsync(Guid resourceId);
        Task AddMarketSellResourceAsync(MarketSellResource marketSellResource);
        Task UpdateMarketSellResourceAsync(MarketSellResource marketSellResource);
        Task DeleteMarketSellResourceAsync(Guid marketSellResourceId);
    }
}
