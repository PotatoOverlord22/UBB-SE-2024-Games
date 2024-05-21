using GameWorld.Entities;

namespace GameWorld.Repositories
{
    public interface IUserAchievementRepository
    {
        Task<List<UserAchievement>> GetAllUserAchievementsAsync(Guid userId);
        Task AddUserAchievementAsync(UserAchievement userAchievement);
        Task UpdateUserAchievementAsync(UserAchievement userAchievement);
        Task DeleteUserAchievementAsync(Guid userAchievementId);
    }
}
