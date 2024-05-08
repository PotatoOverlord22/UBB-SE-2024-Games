using Microsoft.VisualStudio.TestTools.UnitTesting;
using HarvestHaven.Entities;
using System;

namespace HarvestHaven.Entities.Tests
{
    [TestClass()]
    public class MarketBuyItemTests
    {
        [TestMethod()]
        public void MarketBuyItem_Constructor_InitializesProperties()
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
