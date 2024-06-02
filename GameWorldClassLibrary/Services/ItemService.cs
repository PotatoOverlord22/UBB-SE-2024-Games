using GameWorldClassLibrary.Models;
using GameWorldClassLibrary.Repositories;
using GameWorldClassLibrary.Services;

namespace GameWorldClassLibrary.Services
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
