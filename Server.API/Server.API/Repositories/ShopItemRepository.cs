using Microsoft.EntityFrameworkCore;
using GameWorldClassLibrary.Models;
using Server.API.Repositories;
using Server.API.Utils;

public class ShopItemRepository : IShopItemRepository
{
    private readonly GamesContext context;

    public ShopItemRepository(GamesContext context)
    {
        this.context = context;
    }

    public async Task<List<ShopItem>> GetShopItemAsync()
    {
        return await context.ShopItems.ToListAsync();
    }

    public async Task<ShopItem> GetShopItemByIdAsync(Guid id)
    {
        var shopItem = await context.ShopItems.FindAsync(id);

        if (shopItem == null)
        {
            throw new KeyNotFoundException("ShopItem not found");
        }

        return shopItem;
    }
    public async Task AddShopItemAsync(ShopItem shopItem)
    {
        context.ShopItems.Add(shopItem);
        await context.SaveChangesAsync();
    }
    public async Task DeleteShopItemAsync(Guid id)
    {
        var shopItem = context.ShopItems.Find(id);
        if (shopItem == null)
        {
            throw new KeyNotFoundException("ShopItem not found");
        }
        context.ShopItems.Remove(shopItem);
        await context.SaveChangesAsync();
    }

    public async Task UpdateShopItemAsync(Guid id, ShopItem shopItem)
    {
        if (context.ShopItems.Find(id) == null)
        {
            throw new KeyNotFoundException("ShopItem not found");
        }
        context.ShopItems.Update(shopItem);
        await context.SaveChangesAsync();
    }
}
