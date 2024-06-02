using GameWorldClassLibrary.Models;

namespace GameWorldClassLibrary.Services.Interfaces
{
    public interface IItemService
    {
        Task<Item> GetItemByIdAsync(Guid itemId);
        Task<List<Item>> GetAllItemsAsync();
    }
}
