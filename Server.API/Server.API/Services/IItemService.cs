using Server.API.Models;

namespace Server.API.Services
{
    public interface IItemService
    {
        Task<Item> GetItemByIdAsync(Guid id);
        Task<List<Item>> GetItemsAsync();
        void AddItem(Item item);
        void DeleteItem(Guid id);
        void UpdateItem(Guid id, Item item);
    }
}