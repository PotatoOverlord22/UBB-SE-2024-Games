using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using HarvestHaven.Entities;
using System;

namespace HarvestHaven.Entities.Tests
{
    [TestClass()]
    public class InventoryResourceTests
    {
        [TestMethod()]
        public void InventoryResource_Constructor_InitializesProperties()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            Guid userId = Guid.NewGuid();
            Guid resourceId = Guid.NewGuid();
            int quantity = 10;

            // Act
            InventoryResource inventoryResource = new InventoryResource(id, userId, resourceId, quantity);

            // Assert
            Assert.AreEqual(id, inventoryResource.Id);
            Assert.AreEqual(userId, inventoryResource.UserId);
            Assert.AreEqual(resourceId, inventoryResource.ResourceId);
            Assert.AreEqual(quantity, inventoryResource.Quantity);
        }
    }
}
