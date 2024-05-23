using Server.API.Models;

namespace Server.API.Services
{
    public interface IItemService
    {
        Task<Item> GetItemByIdAsync(Guid id);
        Task<List<Item>> GetItemsAsync();
        Task AddItemAsync(Item item);
        Task DeleteItemAsync(Guid id);
        Task UpdateItemAsync(Guid id, Item item);
    }
}