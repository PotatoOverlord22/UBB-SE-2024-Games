using Microsoft.EntityFrameworkCore;
using Server.API.Models;
using Server.API.Services;
using Server.API.Utils;

public class IconRepository : IIconRepository
{
    private readonly GamesContext context;

    public IconRepository(GamesContext context)
    {
        this.context = context;
    }

    public async Task<Icon> GetIconByIdAsync(Guid id)
    {
        var icon = await context.Icons.FindAsync(id);

        if (icon == null)
        {
            throw new KeyNotFoundException("Icon not found");
        }

        return icon;
    }
    public async Task AddIconAsync(Icon icon)
    {
        context.Icons.Add(icon);
        await context.SaveChangesAsync();
    }
    public async Task DeleteIconAsync(Guid id)
    {
        var icon = context.Icons.Find(id);
        if (icon == null)
        {
            throw new KeyNotFoundException("Icon not found");
        }
        context.Icons.Remove(icon);
        await context.SaveChangesAsync();
    }
    public async Task UpdateIconAsync(Guid id, Icon icon)
    {
        if (context.Icons.Find(id) == null)
        {
            throw new KeyNotFoundException("Icon not found");
        }
        context.Icons.Update(icon);
        await context.SaveChangesAsync();
    }

    public async Task<List<Icon>> GetIconsAsync()
    {
        return await context.Icons.ToListAsync();
    }
}
