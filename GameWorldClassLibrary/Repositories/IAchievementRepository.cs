using GameWorldClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameWorldClassLibrary.Repositories
{
    public interface IAchievementRepository
    {
        Task AddAchievement(Achievement achievement);
        Task DeleteAchievementAsync(Guid id);
        Task<Achievement> GetAchievementByIdAsync(Guid id);
        Task<List<Achievement>> GetAchievementsAsync();
        Task UpdateAchievementAsync(Guid id, Achievement achievement);
    }
}