using Microsoft.AspNetCore.Mvc;
using Server.API.Models;

namespace Server.API.Services
{
    public interface IAchievementService
    {
        void AddAchievement(Achievement achievement);
        void DeleteAchievement(Guid id);
        Task<Achievement> GetAchievementByIdAsync(Guid id);
        Task<List<Achievement>> GetAchievementsAsync();
        void UpdateAchievement(Guid id, Achievement achievement);
    }
}