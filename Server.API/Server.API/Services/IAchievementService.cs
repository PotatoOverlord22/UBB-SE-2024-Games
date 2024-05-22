using Microsoft.AspNetCore.Mvc;
using Server.API.Models;

namespace Server.API.Services
{
    public interface IAchievementService
    {
        Task AddAchievement(Achievement achievement);
        Task DeleteAchievementAsync(Guid id);
        Task<Achievement> GetAchievementByIdAsync(Guid id);
        Task<List<Achievement>> GetAchievementsAsync();
        Task UpdateAchievementAsync(Guid id, Achievement achievement);
    }
}