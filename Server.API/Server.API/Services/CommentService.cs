using Microsoft.EntityFrameworkCore;
using Server.API.Models;
using Server.API.Services;
using Server.API.Utils;

public class CommentService : ICommentService
{
    private readonly GamesContext context;

    public CommentService(GamesContext context)
    {
        this.context = context;
    }

    public async Task<List<Comment>> GetAchievementsAsync()
    {
        return await context.Achievements.ToListAsync();
    }

    public async Task<Comment> GetAchievementByIdAsync(Guid id)
    {
        var comment = await context.Achievements.FindAsync(id);

        if (comment == null)
        {
            throw new KeyNotFoundException("Achievement not found");
        }

        return comment;
    }

    public async Task UpdateAchievementAsync(Guid id, Comment achievement)
    {
        if (context.Achievements.Find(id) == null)
        {
            throw new KeyNotFoundException("Achievement not found");
        }

        context.Achievements.Update(achievement);
        await context.SaveChangesAsync();
    }

    public async Task AddAchievement(Comment achievement)
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
