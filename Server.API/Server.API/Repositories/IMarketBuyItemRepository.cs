using Server.API.Models;

namespace Server.API.Services
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
