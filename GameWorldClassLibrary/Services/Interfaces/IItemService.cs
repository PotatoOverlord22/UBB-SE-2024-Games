using GameWorldClassLibrary.Models;

namespace GameWorldClassLibrary.Services
{
    public interface IItemService
    {
        Task<Item> GetItemByIdAsync(Guid itemId);
        Task<List<Item>> GetAllItemsAsync();
    }
}
