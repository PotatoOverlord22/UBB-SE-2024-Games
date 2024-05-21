namespace GameWorld.Entities
{
    public class UserAchievement
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid AchievementId { get; set; }
        public DateTime AchievementRewardedTime { get; set; }

        public UserAchievement(Guid id, Guid userId, Guid achievementId, DateTime createdTime)
        {
            Id = id;
            UserId = userId;
            AchievementId = achievementId;
            AchievementRewardedTime = createdTime;
        }
    }
}
