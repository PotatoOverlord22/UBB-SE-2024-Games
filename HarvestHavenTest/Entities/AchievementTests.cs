using Microsoft.VisualStudio.TestTools.UnitTesting;
using HarvestHaven.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarvestHaven.Entities.Tests
{
    [TestClass()]
    public class AchievementTests
    {
        [TestMethod()]
        public void Achievement_Constructor_InitializesProperties()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            string description = "Test Achievement";
            int rewardCoins = 100;

            // Act
            Achievement achievement = new Achievement(id, description, rewardCoins);

            // Assert
            Assert.AreEqual(id, achievement.Id);
            Assert.AreEqual(description, achievement.Description);
            Assert.AreEqual(rewardCoins, achievement.RewardCoins);
        }
    }
}