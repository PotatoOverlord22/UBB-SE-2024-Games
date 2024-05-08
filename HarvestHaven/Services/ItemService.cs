using HarvestHaven.Repositories;
using HarvestHaven.Entities;

namespace HarvestHaven.Services
{
    public class ItemService : IItemService
    {
        public async Task<Item> GetItemByIdAsync(Guid itemId)
        {
            return await ItemRepository.GetItemByIdAsync(itemId);
        }
        public async Task<List<Item>> GetAllItemsAsync()
        {
            return await ItemRepository.GetAllItemsAsync();
        }
    }
}
