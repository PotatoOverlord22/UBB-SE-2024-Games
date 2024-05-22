using GameWorld.Models;

namespace GameWorld.Entities.Tests
{
    [TestClass()]
    public class ItemTests
    {
        [TestMethod()]
        public void Constructor_WithValidParameters_InitializesProperties()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            ItemType itemType = ItemType.Cow;
            Guid requiredResourceId = Guid.NewGuid();
            Guid interactResourceId = Guid.NewGuid();
            Guid? destroyResourceId = null;

            // Act
            Item item = new Item(id, itemType, requiredResourceId, interactResourceId, destroyResourceId);

            // Assert
            Assert.AreEqual(id, item.Id);
            Assert.AreEqual(itemType, item.ItemType);
            Assert.AreEqual(requiredResourceId, item.ResourceToPlaceId);
            Assert.AreEqual(interactResourceId, item.ResourceToInteractId);
            Assert.AreEqual(destroyResourceId, item.ResourceToDestroyId);
        }
    }

    [TestClass()]
    public class ItemTypeTests
    {
        [TestMethod()]
        public void EnumValues_AreCorrect()
        {
            // Arrange
            ItemType[] expectedValues = { ItemType.Chicken, ItemType.Cow, ItemType.Sheep, ItemType.Duck,
                                          ItemType.WheatSeeds, ItemType.CornSeeds, ItemType.CarrotSeeds, ItemType.TomatoSeeds };

            // Act
            ItemType[] enumValues = (ItemType[])Enum.GetValues(typeof(ItemType));

            // Assert
            CollectionAssert.AreEqual(expectedValues, enumValues);
        }
    }
}
