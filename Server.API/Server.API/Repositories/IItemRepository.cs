using Server.API.Models;

namespace Server.API.Repositories
{
    public interface IItemRepository
    {
        Task<Item> GetItemByIdAsync(Guid id);
        Task<List<Item>> GetItemsAsync();
        Task AddItemAsync(Item item);
        Task DeleteItemAsync(Guid id);
        Task UpdateItemAsync(Guid id, Item item);
    }
}