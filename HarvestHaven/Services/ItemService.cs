using HarvestHaven.Repositories;
using HarvestHaven.Entities;

namespace HarvestHaven.Services
{
    public static class ItemService
    {
        public static async Task<Item> GetItemByIdAsync(Guid itemId)
        {
            return await ItemRepository.GetItemByIdAsync(itemId);
        }
        public static async Task<List<Item>> GetAllItemsAsync()
        {
            return await ItemRepository.GetAllItemsAsync();
        }
    }
}
