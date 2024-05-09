using HarvestHaven.Entities;
using HarvestHaven.Repositories;
using HarvestHaven.Services;
using HarvestHaven.Utils;
using Moq;

namespace HarvestHaven.Tests.Services
{
    [TestClass]
    public class MarketServiceTests
    {
        [TestMethod]
        public async Task BuyItem_UserIsLoggedIn_ItemBoughtSuccessfully()
        {
            // Arrange
            var currentUser = new User(Guid.NewGuid(), "TestUser", 100, 5, 2, DateTime.UtcNow, DateTime.UtcNow);
            GameStateManager.SetCurrentUser(currentUser);

            var itemType = ItemType.CornSeeds;
            var item = new Item(Guid.NewGuid(), itemType, Guid.NewGuid(), Guid.NewGuid(), null);
            var buyPrice = 20;

            var itemRepositoryMock = new Mock<IItemRepository>();
            itemRepositoryMock.Setup(repo => repo.GetItemByTypeAsync(itemType)).ReturnsAsync(item);

            var marketBuyItem = new MarketBuyItem(Guid.NewGuid(), item.Id, buyPrice);
            var marketBuyItemRepositoryMock = new Mock<IMarketBuyItemRepository>();
            marketBuyItemRepositoryMock.Setup(repo => repo.GetMarketBuyItemByItemIdAsync(item.Id)).ReturnsAsync(marketBuyItem);

            var farmCells = new List<FarmCell>();
            var farmCellRepositoryMock = new Mock<IFarmCellRepository>();
            farmCellRepositoryMock.Setup(repo => repo.GetUserFarmCellsAsync(currentUser.Id)).ReturnsAsync(farmCells);

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(repo => repo.UpdateUserAsync(currentUser)).Returns(Task.CompletedTask);

            var achievementServiceMock = new Mock<IAchievementService>();
            achievementServiceMock.Setup(service => service.CheckFarmAchievements()).Returns(Task.CompletedTask);
            achievementServiceMock.Setup(service => service.CheckMarketAchievements()).Returns(Task.CompletedTask);

            var marketService = new MarketService(achievementServiceMock.Object, farmCellRepositoryMock.Object, userRepositoryMock.Object, itemRepositoryMock.Object, marketBuyItemRepositoryMock.Object, null, null, null);

            // Act
            await marketService.BuyItem(1, 1, itemType);

            // Assert
            userRepositoryMock.Verify(repo => repo.UpdateUserAsync(currentUser), Times.Once);
            achievementServiceMock.Verify(service => service.CheckFarmAchievements(), Times.Once);
            achievementServiceMock.Verify(service => service.CheckMarketAchievements(), Times.Once);
            Assert.AreEqual(currentUser.Coins, 100 - buyPrice);
            Assert.AreEqual(currentUser.NrItemsBought, 6);
        }

        [TestMethod]
        public async Task BuyItem_UserIsNotLoggedIn_ThrowsException()
        {
            // Arrange
            GameStateManager.SetCurrentUser(null);
            var marketService = new MarketService(null, null, null, null, null, null, null, null);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(async () => await marketService.BuyItem(1, 1, ItemType.CornSeeds));
        }

        [TestMethod]
        public async Task BuyItem_ItemNotFound_ThrowsException()
        {
            // Arrange
            var currentUser = new User(Guid.NewGuid(), "TestUser", 100, 5, 2, DateTime.UtcNow, DateTime.UtcNow);
            GameStateManager.SetCurrentUser(currentUser);

            var itemType = ItemType.CornSeeds;
            var itemRepositoryMock = new Mock<IItemRepository>();
            itemRepositoryMock.Setup(repo => repo.GetItemByTypeAsync(itemType)).ReturnsAsync((Item)null);

            var marketService = new MarketService(null, null, null, itemRepositoryMock.Object, null, null, null, null);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(async () => await marketService.BuyItem(1, 1, itemType));
        }

        [TestMethod]
        public async Task BuyItem_MarketBuyItemNotFound_ThrowsException()
        {
            // Arrange
            var currentUser = new User(Guid.NewGuid(), "TestUser", 100, 5, 2, DateTime.UtcNow, DateTime.UtcNow);
            GameStateManager.SetCurrentUser(currentUser);

            var itemType = ItemType.CornSeeds;
            var item = new Item(Guid.NewGuid(), itemType, Guid.NewGuid(), Guid.NewGuid(), null);

            var itemRepositoryMock = new Mock<IItemRepository>();
            itemRepositoryMock.Setup(repo => repo.GetItemByTypeAsync(itemType)).ReturnsAsync(item);

            var marketBuyItemRepositoryMock = new Mock<IMarketBuyItemRepository>();
            marketBuyItemRepositoryMock.Setup(repo => repo.GetMarketBuyItemByItemIdAsync(item.Id)).ReturnsAsync((MarketBuyItem)null);

            var marketService = new MarketService(null, null, null, itemRepositoryMock.Object, marketBuyItemRepositoryMock.Object, null, null, null);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(async () => await marketService.BuyItem(1, 1, itemType));
        }
        [TestMethod]
        public async Task BuyItem_NotEnoughMoney_ThrowsException()
        {
            // Arrange
            var currentUser = new User(Guid.NewGuid(), "TestUser", 10, 5, 2, DateTime.UtcNow, DateTime.UtcNow);
            GameStateManager.SetCurrentUser(currentUser);

            var itemType = ItemType.CornSeeds;
            var item = new Item(Guid.NewGuid(), itemType, Guid.NewGuid(), Guid.NewGuid(), null);
            var buyPrice = 20;

            var itemRepositoryMock = new Mock<IItemRepository>();
            itemRepositoryMock.Setup(repo => repo.GetItemByTypeAsync(itemType)).ReturnsAsync(item);

            var marketBuyItem = new MarketBuyItem(Guid.NewGuid(), item.Id, buyPrice);
            var marketBuyItemRepositoryMock = new Mock<IMarketBuyItemRepository>();
            marketBuyItemRepositoryMock.Setup(repo => repo.GetMarketBuyItemByItemIdAsync(item.Id)).ReturnsAsync(marketBuyItem);

            var marketService = new MarketService(null, null, null, itemRepositoryMock.Object, marketBuyItemRepositoryMock.Object, null, null, null);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(async () => await marketService.BuyItem(1, 1, itemType));
        }
        [TestMethod]
        public async Task BuyItem_CellOccupied_ThrowsException()
        {
            // Arrange
            var currentUser = new User(Guid.NewGuid(), "TestUser", 100, 5, 2, DateTime.UtcNow, DateTime.UtcNow);
            GameStateManager.SetCurrentUser(currentUser);

            var itemType = ItemType.CornSeeds;
            var item = new Item(Guid.NewGuid(), itemType, Guid.NewGuid(), Guid.NewGuid(), null);
            var buyPrice = 20;

            var itemRepositoryMock = new Mock<IItemRepository>();
            itemRepositoryMock.Setup(repo => repo.GetItemByTypeAsync(itemType)).ReturnsAsync(item);

            var marketBuyItem = new MarketBuyItem(Guid.NewGuid(), item.Id, buyPrice);
            var marketBuyItemRepositoryMock = new Mock<IMarketBuyItemRepository>();
            marketBuyItemRepositoryMock.Setup(repo => repo.GetMarketBuyItemByItemIdAsync(item.Id)).ReturnsAsync(marketBuyItem);

            var farmCells = new List<FarmCell> { new FarmCell(Guid.NewGuid(), currentUser.Id, 1, 1, Guid.NewGuid(), null, null) };
            var farmCellRepositoryMock = new Mock<IFarmCellRepository>();
            farmCellRepositoryMock.Setup(repo => repo.GetUserFarmCellsAsync(currentUser.Id)).ReturnsAsync(farmCells);

            var marketService = new MarketService(null, farmCellRepositoryMock.Object, null, itemRepositoryMock.Object, marketBuyItemRepositoryMock.Object, null, null, null);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(async () => await marketService.BuyItem(1, 1, itemType));
        }

        [TestMethod]
        public async Task SellResource_UserIsLoggedIn_ResourceSoldSuccessfully()
        {
            // Arrange
            var currentUser = new User(Guid.NewGuid(), "TestUser", 100, 5, 2, DateTime.UtcNow, DateTime.UtcNow);
            GameStateManager.SetCurrentUser(currentUser);

            var resourceType = ResourceType.Wheat;
            var resource = new Resource(Guid.NewGuid(), resourceType);
            var sellPrice = 20;

            var resourceRepositoryMock = new Mock<IResourceRepository>();
            resourceRepositoryMock.Setup(repo => repo.GetResourceByTypeAsync(resourceType)).ReturnsAsync(resource);

            var marketSellResource = new MarketSellResource(Guid.NewGuid(), resource.Id, sellPrice);
            var marketSellResourceRepositoryMock = new Mock<IMarketSellResourceRepository>();
            marketSellResourceRepositoryMock.Setup(repo => repo.GetMarketSellResourceByResourceIdAsync(resource.Id)).ReturnsAsync(marketSellResource);

            var inventoryResource = new InventoryResource(Guid.NewGuid(), currentUser.Id, resource.Id, 5);
            var inventoryResourceRepositoryMock = new Mock<IInventoryResourceRepository>();
            inventoryResourceRepositoryMock.Setup(repo => repo.GetUserResourceByResourceIdAsync(currentUser.Id, resource.Id)).ReturnsAsync(inventoryResource);
            inventoryResourceRepositoryMock.Setup(repo => repo.UpdateUserResourceAsync(inventoryResource)).Returns(Task.CompletedTask);

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(repo => repo.UpdateUserAsync(currentUser)).Returns(Task.CompletedTask);

            var achievementServiceMock = new Mock<IAchievementService>();
            achievementServiceMock.Setup(service => service.CheckInventoryAchievements()).Returns(Task.CompletedTask);

            var marketService = new MarketService(achievementServiceMock.Object, null, userRepositoryMock.Object, null, null, inventoryResourceRepositoryMock.Object, marketSellResourceRepositoryMock.Object, resourceRepositoryMock.Object);

            // Act
            await marketService.SellResource(resourceType);

            // Assert
            userRepositoryMock.Verify(repo => repo.UpdateUserAsync(currentUser), Times.Once);
            achievementServiceMock.Verify(service => service.CheckInventoryAchievements(), Times.Once);
            Assert.AreEqual(currentUser.Coins, 100 + sellPrice);
        }

        [TestMethod]
        public async Task SellResource_UserIsNotLoggedIn_ThrowsException()
        {
            // Arrange
            GameStateManager.SetCurrentUser(null);
            var marketService = new MarketService(null, null, null, null, null, null, null, null);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(async () => await marketService.SellResource(ResourceType.Wheat));
        }

        [TestMethod]
        public async Task SellResource_ResourceNotFound_ThrowsException()
        {
            // Arrange
            var currentUser = new User(Guid.NewGuid(), "TestUser", 100, 5, 2, DateTime.UtcNow, DateTime.UtcNow);
            GameStateManager.SetCurrentUser(currentUser);

            var resourceType = ResourceType.Wheat;
            var resourceRepositoryMock = new Mock<IResourceRepository>();
            resourceRepositoryMock.Setup(repo => repo.GetResourceByTypeAsync(resourceType)).ReturnsAsync((Resource)null);

            var marketService = new MarketService(null, null, null, null, null, null, null, resourceRepositoryMock.Object);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(async () => await marketService.SellResource(resourceType));
        }

        [TestMethod]
        public async Task SellResource_MarketSellResourceNotFound_ThrowsException()
        {
            // Arrange
            var currentUser = new User(Guid.NewGuid(), "TestUser", 100, 5, 2, DateTime.UtcNow, DateTime.UtcNow);
            GameStateManager.SetCurrentUser(currentUser);

            var resourceType = ResourceType.Wheat;
            var resource = new Resource(Guid.NewGuid(), resourceType);
            var resourceRepositoryMock = new Mock<IResourceRepository>();
            resourceRepositoryMock.Setup(repo => repo.GetResourceByTypeAsync(resourceType)).ReturnsAsync(resource);

            var marketSellResourceRepositoryMock = new Mock<IMarketSellResourceRepository>();
            marketSellResourceRepositoryMock.Setup(repo => repo.GetMarketSellResourceByResourceIdAsync(resource.Id)).ReturnsAsync((MarketSellResource)null);

            var marketService = new MarketService(null, null, null, null, null, null, marketSellResourceRepositoryMock.Object, resourceRepositoryMock.Object);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(async () => await marketService.SellResource(resourceType));
        }

        [TestMethod]
        public async Task SellResource_UserDoesNotOwnResource_ThrowsException()
        {
            // Arrange
            var currentUser = new User(Guid.NewGuid(), "TestUser", 100, 5, 2, DateTime.UtcNow, DateTime.UtcNow);
            GameStateManager.SetCurrentUser(currentUser);

            var resourceType = ResourceType.Wheat;
            var resource = new Resource(Guid.NewGuid(), resourceType);
            var marketSellResource = new MarketSellResource(Guid.NewGuid(), resource.Id, 20);

            var resourceRepositoryMock = new Mock<IResourceRepository>();
            resourceRepositoryMock.Setup(repo => repo.GetResourceByTypeAsync(resourceType)).ReturnsAsync(resource);

            var marketSellResourceRepositoryMock = new Mock<IMarketSellResourceRepository>();
            marketSellResourceRepositoryMock.Setup(repo => repo.GetMarketSellResourceByResourceIdAsync(resource.Id)).ReturnsAsync(marketSellResource);

            var inventoryResourceRepositoryMock = new Mock<IInventoryResourceRepository>();
            inventoryResourceRepositoryMock.Setup(repo => repo.GetUserResourceByResourceIdAsync(currentUser.Id, resource.Id)).ReturnsAsync((InventoryResource)null);

            var marketService = new MarketService(null, null, null, null, null, inventoryResourceRepositoryMock.Object, marketSellResourceRepositoryMock.Object, resourceRepositoryMock.Object);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(async () => await marketService.SellResource(resourceType));
        }

    }
}
