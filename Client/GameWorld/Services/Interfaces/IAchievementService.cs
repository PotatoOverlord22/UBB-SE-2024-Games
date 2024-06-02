using GameWorldClassLibrary.Models;

namespace GameWorld.Services
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
