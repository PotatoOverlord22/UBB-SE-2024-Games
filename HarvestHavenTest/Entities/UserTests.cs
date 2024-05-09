using Microsoft.VisualStudio.TestTools.UnitTesting;
using HarvestHaven.Entities;
using System;

namespace HarvestHaven.Entities.Tests
{
    [TestClass()]
    public class UserTests
    {
        [TestMethod()]
        public void Constructor_WithValidParameters_InitializesProperties()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            string username = "testuser";
            int coins = 100;
            int nrItemsBought = 5;
            int nrTradesPerformed = 2;
            DateTime? tradeHallUnlockTime = DateTime.UtcNow;
            DateTime? lastTimeReceivedWater = DateTime.UtcNow;

            // Act
            User user = new User(id, username, coins, nrItemsBought, nrTradesPerformed, tradeHallUnlockTime, lastTimeReceivedWater);

            // Assert
            Assert.AreEqual(id, user.Id);
            Assert.AreEqual(username, user.Username);
            Assert.AreEqual(coins, user.Coins);
            Assert.AreEqual(nrItemsBought, user.AmountOfItemsBought);
            Assert.AreEqual(nrTradesPerformed, user.AmountOfTradesPerformed);
            Assert.AreEqual(tradeHallUnlockTime, user.TradeHallUnlockTime);
            Assert.AreEqual(lastTimeReceivedWater, user.LastTimeReceivedWater);
        }
    }
}
