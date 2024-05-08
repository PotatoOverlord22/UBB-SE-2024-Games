using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using HarvestHaven.Entities;
using System;

namespace HarvestHaven.Entities.Tests
{
    [TestClass()]
    public class TradeTests
    {
        [TestMethod()]
        public void Constructor_WithValidParameters_InitializesProperties()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            Guid userId = Guid.NewGuid();
            Guid givenResourceId = Guid.NewGuid();
            int givenResourceQuantity = 5;
            Guid requestedResourceId = Guid.NewGuid();
            int requestedResourceQuantity = 10;
            DateTime createdTime = DateTime.UtcNow;
            bool isCompleted = false;

            // Act
            Trade trade = new Trade(id, userId, givenResourceId, givenResourceQuantity, requestedResourceId, requestedResourceQuantity, createdTime, isCompleted);

            // Assert
            Assert.AreEqual(id, trade.Id);
            Assert.AreEqual(userId, trade.UserId);
            Assert.AreEqual(givenResourceId, trade.GivenResourceId);
            Assert.AreEqual(givenResourceQuantity, trade.GivenResourceQuantity);
            Assert.AreEqual(requestedResourceId, trade.RequestedResourceId);
            Assert.AreEqual(requestedResourceQuantity, trade.RequestedResourceQuantity);
            Assert.AreEqual(createdTime, trade.CreatedTime);
            Assert.AreEqual(isCompleted, trade.IsCompleted);
        }
    }
}
