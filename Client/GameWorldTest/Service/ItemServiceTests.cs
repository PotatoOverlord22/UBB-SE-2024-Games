using GameWorld.Models;
using GameWorld.Repositories;
using Moq;

namespace GameWorld.Services.Tests
{
    [TestClass]
    public class ItemServiceTests
    {
        [TestMethod]
        public async Task GetItemByIdAsync_ItemExists_ReturnsItem()
        {
            // Arrange
            var itemId = Guid.NewGuid();
            var expectedItem = new Item(itemId, ItemType.CornSeeds, Guid.NewGuid(), Guid.NewGuid(), null);

            var itemRepositoryMock = new Mock<IItemRepository>();
            itemRepositoryMock.Setup(repo => repo.GetItemByIdAsync(itemId)).ReturnsAsync(expectedItem);

            var itemService = new ItemService(itemRepositoryMock.Object);

            // Act
            var result = await itemService.GetItemByIdAsync(itemId);

            // Assert
            Assert.AreEqual(expectedItem, result);
        }

        [TestMethod]
        public async Task GetItemByIdAsync_ItemDoesNotExist_ReturnsNull()
        {
            // Arrange
            var itemId = Guid.NewGuid();

            var itemRepositoryMock = new Mock<IItemRepository>();
            itemRepositoryMock.Setup(repo => repo.GetItemByIdAsync(itemId)).ReturnsAsync((Item)null);

            var itemService = new ItemService(itemRepositoryMock.Object);

            // Act
            var result = await itemService.GetItemByIdAsync(itemId);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task GetAllItemsAsync_ReturnsListOfItems()
        {
            // Arrange
            var expectedItems = new List<Item>
            {
                new Item(Guid.NewGuid(), ItemType.CornSeeds, Guid.NewGuid(), Guid.NewGuid(), null),
                new Item(Guid.NewGuid(), ItemType.WheatSeeds, Guid.NewGuid(), Guid.NewGuid(), null),
                new Item(Guid.NewGuid(), ItemType.Cow, Guid.NewGuid(), Guid.NewGuid(), null)
            };

            var itemRepositoryMock = new Mock<IItemRepository>();
            itemRepositoryMock.Setup(repo => repo.GetAllItemsAsync()).ReturnsAsync(expectedItems);

            var itemService = new ItemService(itemRepositoryMock.Object);

            // Act
            var result = await itemService.GetAllItemsAsync();

            // Assert
            CollectionAssert.AreEqual(expectedItems, result);
        }
    }
}
