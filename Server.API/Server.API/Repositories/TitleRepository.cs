using Microsoft.EntityFrameworkCore;
using GameWorldClassLibrary.Models;
using Server.API.Repositories;
using Server.API.Utils;

public class TitleRepository : ITitleRepository
{
    private readonly GamesContext context;

    public TitleRepository(GamesContext context)
    {
        this.context = context;
    }

    public async Task<List<Title>> GetTitleAsync()
    {
        return await context.Titles.ToListAsync();
    }

    public async Task<Title> GetTitleByIdAsync(Guid id)
    {
        var title = await context.Titles.FindAsync(id);

        if (title == null)
        {
            throw new KeyNotFoundException("ITitle not found");
        }

        return title;
    }
    public async Task AddTitleAsync(Title title)
    {
        context.Titles.Add(title);
        await context.SaveChangesAsync();
    }
    public async Task DeleteTitleAsync(Guid id)
    {
        var title = context.Titles.Find(id);
        if (title == null)
        {
            throw new KeyNotFoundException("Title not found");
        }
        context.Titles.Remove(title);
        await context.SaveChangesAsync();
    }
    public async Task UpdateTitleAsync(Guid id, Title title)
    {
        if (context.Titles.Find(id) == null)
        {
            throw new KeyNotFoundException("Title not found");
        }
        context.Titles.Update(title);
        await context.SaveChangesAsync();
    }
}
