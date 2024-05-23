using Server.API.Models;

namespace Server.API.Services
{
    public interface IMarketSellResourceService
    {
        Task<List<MarketSellResource>> GetAllMarketSellResourcesAsync();
        Task<MarketBuyItem> GetMarketSellResourceByResourceIdAsync(Guid resourceId);
        Task AddMarketSellResourceAsync(IMarketSellResource marketSellResource);
        Task UpdateMarketSellResourceAsync(MarketSellResource marketSellResource);
        Task DeleteMarketSellResourceAsync(Guid marketSellResourceId);
    }
}
