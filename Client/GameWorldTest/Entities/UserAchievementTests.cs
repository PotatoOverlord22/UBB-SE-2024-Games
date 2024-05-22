using GameWorld.Models;

namespace GameWorld.Entities.Tests
{
    [TestClass()]
    public class UserAchievementTests
    {
        [TestMethod()]
        public void Constructor_WithValidParameters_InitializesProperties()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            Guid userId = Guid.NewGuid();
            Guid achievementId = Guid.NewGuid();
            DateTime createdTime = DateTime.UtcNow;

            // Act
            UserAchievement userAchievement = new UserAchievement(id, userId, achievementId, createdTime);

            // Assert
            Assert.AreEqual(id, userAchievement.Id);
            Assert.AreEqual(userId, userAchievement.UserId);
            Assert.AreEqual(achievementId, userAchievement.AchievementId);
            Assert.AreEqual(createdTime, userAchievement.AchievementRewardedTime);
        }
    }
}
