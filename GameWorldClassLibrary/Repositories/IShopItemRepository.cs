using GameWorldClassLibrary.Models;

namespace GameWorldClassLibrary.Repositories
{
    public interface IShopItemRepository
    {
        Task<ShopItem> GetShopItemByIdAsync(Guid id);
        Task<List<ShopItem>> GetShopItemAsync();
        Task AddShopItemAsync(ShopItem shopItem);
        Task DeleteShopItemAsync(Guid id);
        Task UpdateShopItemAsync(Guid id, ShopItem shopItem);
    }
}