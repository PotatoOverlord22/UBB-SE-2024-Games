using Microsoft.EntityFrameworkCore;
using Server.API.Models;
using Server.API.Services;
using Server.API.Utils;

public class ItemService : IItemService
{
    private readonly GamesContext context;

    public ItemService(GamesContext context)
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
    public void AddItem(Item item)
    {
        context.Items.Add(item);
    }
    public void DeleteItem(Guid id)
    {
        var item = context.Items.Find(id);
        if (item == null)
        {
            throw new KeyNotFoundException("Item not found");
        }
        context.Items.Remove(item);
    }
    public void UpdateItem(Guid id, Item item)
    {
        if (context.Items.Find(id) == null)
        {
            throw new KeyNotFoundException("Item not found");
        }
        context.Items.Update(item);
    }
}
