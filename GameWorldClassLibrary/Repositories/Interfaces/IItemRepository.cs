using GameWorldClassLibrary.Models;

namespace GameWorldClassLibrary.Repositories.Interfaces
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
