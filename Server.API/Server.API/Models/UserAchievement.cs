using System.ComponentModel.DataAnnotations.Schema;

namespace Server.API.Models
{
    public class UserAchievement
    {
        public Guid Id { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        [ForeignKey("AchievementId")]
        public Achievement Achievement { get; set; }
        public DateTime AchievementRewardedTime { get; set; }
    }
}
