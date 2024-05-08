using HarvestHaven.Entities;

namespace HarvestHaven.Services
{
    public interface IItemService
    {
        Task<Item> GetItemByIdAsync(Guid itemId);
        Task<List<Item>> GetAllItemsAsync();
    }
}
