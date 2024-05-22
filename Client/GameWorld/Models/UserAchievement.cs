namespace GameWorld.Models
{
    public class UserAchievement
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public Achievement Achievement { get; set; }
        public DateTime AchievementRewardedTime { get; set; }

        public UserAchievement(Guid id, User user, Achievement achievement, DateTime createdTime)
        {
            Id = id;
            User = user;
            Achievement = achievement;
            AchievementRewardedTime = createdTime;
        }
    }
}
