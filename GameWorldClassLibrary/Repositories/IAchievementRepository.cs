using GameWorldClassLibrary.Models;

namespace GameWorldClassLibrary.Repositories
{
    public interface IAchievementRepository
    {
        Task<List<Achievement>> GetAllAchievementsAsync();
        Task<Achievement> GetAchievementByIdAsync(Guid achievementId);
        Task AddAchievementAsync(Achievement achievement);
        Task UpdateAchievementAsync(Achievement achievement);
        Task DeleteAchievementAsync(Guid achievementId);
    }
}
