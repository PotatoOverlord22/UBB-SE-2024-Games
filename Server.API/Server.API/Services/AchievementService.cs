using Microsoft.EntityFrameworkCore;
using Server.API.Models;
using Server.API.Services;
using Server.API.Utils;

public class AchievementService : IAchievementService
{
    private readonly GamesContext context;

    public AchievementService(GamesContext context)
    {
        this.context = context;
    }

    public async Task<List<Achievement>> GetAchievementsAsync()
    {
        return await context.Achievements.ToListAsync();
    }

    public async Task<Achievement> GetAchievementByIdAsync(Guid id)
    {
        var achievement = await context.Achievements.FindAsync(id);

        if (achievement == null)
        {
            throw new KeyNotFoundException("Achievement not found");
        }

        return achievement;
    }

    public async Task UpdateAchievementAsync(Guid id, Achievement achievement)
    {
        if (context.Achievements.Find(id) == null)
        {
            throw new KeyNotFoundException("Achievement not found");
        }

        context.Achievements.Update(achievement);
        await context.SaveChangesAsync();
    }

    public async Task AddAchievement(Achievement achievement)
    {
        context.Achievements.Add(achievement);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAchievementAsync(Guid id)
    {
        var achievement = context.Achievements.Find(id);
        if (achievement == null)
        {
            throw new KeyNotFoundException("Achievement not found");
        }
        context.Achievements.Remove(achievement);
        await context.SaveChangesAsync();
    }
}
