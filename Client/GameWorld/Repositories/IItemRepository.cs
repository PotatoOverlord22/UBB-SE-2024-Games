using GameWorldClassLibrary.Models;

namespace GameWorld.Repositories
{
    public interface IItemRepository
    {
        Task<List<Item>> GetAllItemsAsync();
        Task<Item> GetItemByIdAsync(Guid itemId);
        Task<Item> GetItemByTypeAsync(ItemType itemType);
        Task CreateItemAsync(Item item);
        Task UpdateItemAsync(Item item);
        Task DeleteItemAsync(Guid itemId);
    }
}
