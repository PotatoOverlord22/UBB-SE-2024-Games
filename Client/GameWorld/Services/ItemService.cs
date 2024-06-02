using GameWorldClassLibrary.Models;
using GameWorld.Repositories;

namespace GameWorld.Services
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
