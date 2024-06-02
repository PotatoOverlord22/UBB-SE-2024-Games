using Microsoft.EntityFrameworkCore;
using GameWorldClassLibrary.Models;
using GameWorldClassLibrary.Utils;

namespace GameWorldClassLibrary.Repositories
{
    public class AchievementRepositoryDB : IAchievementRepository
    {
        private readonly GamesContext context;

        public AchievementRepositoryDB(GamesContext context)
        {
            this.context = context;
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

        public async Task UpdateAchievementAsync(Achievement achievement)
        {
            if (context.Achievements.Find(achievement.Id) == null)
            {
                throw new KeyNotFoundException("Achievement not found");
            }

            context.Achievements.Update(achievement);
            await context.SaveChangesAsync();
        }

        public async Task AddAchievementAsync(Achievement achievement)
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

        public Task<List<Achievement>> GetAllAchievementsAsync()
        {
            return context.Achievements.ToListAsync();
        }
    }
}
