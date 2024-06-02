using GameWorldClassLibrary.Models;
using GameWorldClassLibrary.Repositories;
using GameWorldClassLibrary.Utils;
using Microsoft.EntityFrameworkCore;

public class ItemRepository : IItemRepository
{
    private readonly GamesContext context;

    public ItemRepository(GamesContext context)
    {
        this.context = context;
    }

    public async Task<List<Item>> GetItemsAsync()
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
    public async Task AddItemAsync(Item item)
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
    public async Task UpdateItemAsync(Guid id, Item item)
    {
        if (context.Items.Find(id) == null)
        {
            throw new KeyNotFoundException("Item not found");
        }
        context.Items.Update(item);
        await context.SaveChangesAsync();
    }
}
