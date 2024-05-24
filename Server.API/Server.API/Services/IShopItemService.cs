using Server.API.Models;

namespace Server.API.Services
{
    public interface IShopItemService
    {
        Task<ShopItem> GetShopItemByIdAsync(Guid id);
        Task<List<ShopItem>> GetShopItemAsync();
        Task AddShopItemAsync(ShopItem shopItem);
        Task DeleteShopItemAsync(Guid id);
        Task UpdateShopItemAsync(Guid id, ShopItem shopItem);
    }
}