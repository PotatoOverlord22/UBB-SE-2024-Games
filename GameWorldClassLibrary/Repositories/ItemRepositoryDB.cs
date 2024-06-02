using Microsoft.EntityFrameworkCore;
using GameWorldClassLibrary.Models;
using GameWorldClassLibrary.Utils;

namespace GameWorldClassLibrary.Repositories
{
    public class ItemRepositoryDB : IItemRepository
    {
        private readonly GamesContext context;

        public ItemRepositoryDB(GamesContext context)
        {
            this.context = context;
        }

        public async Task<List<Item>> GetAllItemsAsync()
        {
            return await context.Items.ToListAsync();
        }

        public async Task<Item> GetItemByIdAsync(Guid id)
        {
            var item = await context.Items.FindAsync(id);

            if (item == null)
            {
                throw new KeyNotFoundException("Item not found");
            }

            return item;
        }
        public async Task CreateItemAsync(Item item)
        {
            context.Items.Add(item);
            await context.SaveChangesAsync();
        }
        public async Task DeleteItemAsync(Guid id)
        {
            var item = context.Items.Find(id);
            if (item == null)
            {
                throw new KeyNotFoundException("Item not found");
            }
            context.Items.Remove(item);
            await context.SaveChangesAsync();
        }
        public async Task UpdateItemAsync(Item item)
        {
            if (context.Items.Find(item.Id) == null)
            {
                throw new KeyNotFoundException("Item not found");
            }
            context.Items.Update(item);
            await context.SaveChangesAsync();
        }
        public Task<Item> GetItemByTypeAsync(ItemType itemType)
        {
            var item = context.Items.FirstOrDefault(i => i.ItemType == itemType);
            if (item == null)
            {
                throw new KeyNotFoundException("Item not found");
            }
            return Task.FromResult(item);
        }
    }
}
