using Microsoft.VisualStudio.TestTools.UnitTesting;
using HarvestHaven.Entities;
using System;

namespace HarvestHaven.Entities.Tests
{
    [TestClass()]
    public class ResourceTests
    {
        [TestMethod()]
        public void Resource_Constructor_InitializesProperties()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            ResourceType resourceType = ResourceType.Wheat;

            // Act
            Resource resource = new Resource(id, resourceType);

            // Assert
            Assert.AreEqual(id, resource.Id);
            Assert.AreEqual(resourceType, resource.ResourceType);
        }
    }

    [TestClass()]
    public class ResourceTypeTests
    {
        [TestMethod()]
        public void ResourceType_EnumValues_AreCorrect()
        {
            // Arrange
            ResourceType[] expectedValues = {
                ResourceType.Water,
                ResourceType.Wheat,
                ResourceType.Corn,
                ResourceType.Carrot,
                ResourceType.Tomato,
                ResourceType.ChickenEgg,
                ResourceType.DuckEgg,
                ResourceType.SheepWool,
                ResourceType.CowMilk,
                ResourceType.ChickenMeat,
                ResourceType.DuckMeat,
                ResourceType.Mutton,
                ResourceType.Steak
            };

            // Act
            ResourceType[] enumValues = (ResourceType[])Enum.GetValues(typeof(ResourceType));

            // Assert
            CollectionAssert.AreEqual(expectedValues, enumValues);
        }
    }
}
