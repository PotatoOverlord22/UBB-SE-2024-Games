using GameWorld.Models;

namespace GameWorld.Entities.Tests
{
    [TestClass()]
    public class MarketBuyItemTests
    {
        [TestMethod()]
        public void Constructor_WithValidParameters_InitializesProperties()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            Guid itemId = Guid.NewGuid();
            int buyPrice = 50;

            // Act
            MarketBuyItem marketBuyItem = new MarketBuyItem(id, itemId, buyPrice);

            // Assert
            Assert.AreEqual(id, marketBuyItem.Id);
            Assert.AreEqual(itemId, marketBuyItem.ItemId);
            Assert.AreEqual(buyPrice, marketBuyItem.BuyPrice);
        }
    }
}
