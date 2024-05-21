using GameWorld.Entities;

namespace GameWorld.Services
{
    public interface IItemService
    {
        Task<Item> GetItemByIdAsync(Guid itemId);
        Task<List<Item>> GetAllItemsAsync();
    }
}
