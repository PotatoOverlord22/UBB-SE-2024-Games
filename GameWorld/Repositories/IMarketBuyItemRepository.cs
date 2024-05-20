using GameWorld.Entities;

namespace GameWorld.Repositories
{
    public interface IMarketBuyItemRepository
    {
        Task<List<MarketBuyItem>> GetAllMarketBuyItemsAsync();
        Task<MarketBuyItem> GetMarketBuyItemByItemIdAsync(Guid itemId);
        Task AddMarketBuyItemAsync(MarketBuyItem marketBuyItem);
        Task UpdateMarketBuyItemAsync(MarketBuyItem marketBuyItem);
        Task DeleteMarketBuyItemAsync(Guid marketBuyItemId);
    }
}
