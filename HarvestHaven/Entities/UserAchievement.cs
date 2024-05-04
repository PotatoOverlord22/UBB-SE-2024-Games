namespace HarvestHaven.Entities
{
    public class UserAchievement
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid AchievementId { get; set; }
        public DateTime CreatedTime { get; set; }

        public UserAchievement(Guid id, Guid userId, Guid achievementId, DateTime createdTime)
        {
            Id = id;
            UserId = userId;
            AchievementId = achievementId;
            CreatedTime = createdTime;
        }
    }
}
