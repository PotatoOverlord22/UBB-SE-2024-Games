using HarvestHaven.Repositories;
using HarvestHaven.Entities;

namespace HarvestHaven.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository itemRepository;
        public ItemService(IItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;
        }
        public async Task<Item> GetItemByIdAsync(Guid itemId)
        {
            return await itemRepository.GetItemByIdAsync(itemId);
        }
        public async Task<List<Item>> GetAllItemsAsync()
        {
            return await itemRepository.GetAllItemsAsync();
        }
    }
}
