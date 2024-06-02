using GameWorldClassLibrary.Models;

namespace GameWorldClassLibrary.Services
{
    public interface IAchievementService
    {
        Task<List<Achievement>> GetAllAchievementsAsync();
        Task<Dictionary<UserAchievement, Achievement>> GetUserAchievements();
        Task CheckFarmAchievements();
        Task CheckTradeAchievements(Guid otherUserInvolvedId);
        Task CheckInventoryAchievements();
        Task CheckMarketAchievements();
    }
}
