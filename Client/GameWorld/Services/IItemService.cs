using GameWorld.Models;

namespace GameWorld.Services
{
    public interface IItemService
    {
        Task<Item> GetItemByIdAsync(Guid itemId);
        Task<List<Item>> GetAllItemsAsync();
    }
}
