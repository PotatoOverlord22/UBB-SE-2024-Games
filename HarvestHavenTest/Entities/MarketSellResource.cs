using Microsoft.VisualStudio.TestTools.UnitTesting;
using HarvestHaven.Entities;
using System;

namespace HarvestHaven.Entities.Tests
{
    [TestClass()]
    public class MarketSellResourceTests
    {
        [TestMethod()]
        public void Constructor_WithValidParameters_InitializesProperties()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            Guid resourceId = Guid.NewGuid();
            int sellPrice = 20;

            // Act
            MarketSellResource marketSellResource = new MarketSellResource(id, resourceId, sellPrice);

            // Assert
            Assert.AreEqual(id, marketSellResource.Id);
            Assert.AreEqual(resourceId, marketSellResource.ResourceId);
            Assert.AreEqual(sellPrice, marketSellResource.SellPrice);
        }
    }
}
