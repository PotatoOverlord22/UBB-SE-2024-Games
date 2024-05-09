using Microsoft.VisualStudio.TestTools.UnitTesting;
using HarvestHaven.Entities;
using HarvestHaven.Repositories;
using HarvestHaven.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HarvestHaven.Utils;

namespace HarvestHaven.Tests.Services
{
    [TestClass]
    public class TradeServiceTests
    {
        [TestMethod]
        public async Task GetAllTradesExceptCreatedByLoggedUser_ReturnsListOfTrades()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var currentUser = new User(userId, "TestUser", 100, 5, 2, DateTime.Now, DateTime.Now);
            GameStateManager.SetCurrentUser(currentUser);
            var trades = new List<Trade>
            {
                new Trade(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), 10, Guid.NewGuid(), 5, DateTime.Now, false),
                new Trade(Guid.NewGuid(), userId, Guid.NewGuid(), 8, Guid.NewGuid(), 3, DateTime.Now, false)
            };

            var tradeRepositoryMock = new Mock<ITradeRepository>();
            tradeRepositoryMock.Setup(repo => repo.GetAllTradesExceptCreatedByUser(userId)).ReturnsAsync(trades);

            var achievementServiceMock = new Mock<IAchievementService>();
            var inventoryResourceRepositoryMock = new Mock<IInventoryResourceRepository>();
            var resourceRepositoryMock = new Mock<IResourceRepository>();
            var userRepositoryMock = new Mock<IUserRepository>();

            var tradeService = new TradeService(achievementServiceMock.Object, tradeRepositoryMock.Object, inventoryResourceRepositoryMock.Object, resourceRepositoryMock.Object, userRepositoryMock.Object);

            // Act
            var result = await tradeService.GetAllTradesExceptCreatedByLoggedUser();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(trades.Count, result.Count);
        }

        [TestMethod]
        public async Task GetUserTradeAsync_ReturnsUserTrade()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var trade = new Trade(Guid.NewGuid(), userId, Guid.NewGuid(), 10, Guid.NewGuid(), 5, DateTime.Now, false);

            var tradeRepositoryMock = new Mock<ITradeRepository>();
            tradeRepositoryMock.Setup(repo => repo.GetUserTradeAsync(userId)).ReturnsAsync(trade);

            var achievementServiceMock = new Mock<IAchievementService>();
            var inventoryResourceRepositoryMock = new Mock<IInventoryResourceRepository>();
            var resourceRepositoryMock = new Mock<IResourceRepository>();
            var userRepositoryMock = new Mock<IUserRepository>();

            var tradeService = new TradeService(achievementServiceMock.Object, tradeRepositoryMock.Object, inventoryResourceRepositoryMock.Object, resourceRepositoryMock.Object, userRepositoryMock.Object);

            // Act
            var result = await tradeService.GetUserTradeAsync(userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(trade, result);
        }

        [TestMethod]
        public async Task CreateTradeAsync_ValidTrade_CreatesTradeAndUpdatesUserResource()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var givenResourceType = ResourceType.ChickenEgg;
            var givenResourceQuantity = "10";
            var requestedResourceType = ResourceType.CowMilk;
            var requestedResourceQuantity = "5";

            var givenResource = new Resource(Guid.NewGuid(), givenResourceType);
            var requestedResource = new Resource(Guid.NewGuid(), requestedResourceType);

            var userGivenResource = new InventoryResource(Guid.NewGuid(), userId, givenResource.Id, 20);

            var resourceRepositoryMock = new Mock<IResourceRepository>();
            resourceRepositoryMock.Setup(repo => repo.GetResourceByTypeAsync(givenResourceType)).ReturnsAsync(givenResource);
            resourceRepositoryMock.Setup(repo => repo.GetResourceByTypeAsync(requestedResourceType)).ReturnsAsync(requestedResource);

            var inventoryResourceRepositoryMock = new Mock<IInventoryResourceRepository>();
            inventoryResourceRepositoryMock.Setup(repo => repo.GetUserResourceByResourceIdAsync(userId, givenResource.Id)).ReturnsAsync(userGivenResource);

            var tradeRepositoryMock = new Mock<ITradeRepository>();
            var achievementServiceMock = new Mock<IAchievementService>();
            var userRepositoryMock = new Mock<IUserRepository>();

            var currentUser = new User(userId, "testuser", 100, 5, 2, DateTime.Now, DateTime.Now);
            GameStateManager.SetCurrentUser(currentUser);

            var tradeService = new TradeService(achievementServiceMock.Object, tradeRepositoryMock.Object, inventoryResourceRepositoryMock.Object, resourceRepositoryMock.Object, userRepositoryMock.Object);

            // Act
            await tradeService.CreateTradeAsync(givenResourceType, givenResourceQuantity, requestedResourceType, requestedResourceQuantity);

            // Modify userGivenResource to reflect the change in quantity
            userGivenResource.Quantity -= Convert.ToInt32(givenResourceQuantity);

            // Assert
            tradeRepositoryMock.Verify(repo => repo.CreateTradeAsync(It.IsAny<Trade>()), Times.Once);
            inventoryResourceRepositoryMock.Verify(repo => repo.UpdateUserResourceAsync(userGivenResource), Times.Once);
        }



        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task CreateTradeAsync_UserNotLoggedIn_ThrowsException()
        {
            // Arrange
            var givenResourceType = ResourceType.ChickenEgg;
            var givenResourceQuantity = "10";
            var requestedResourceType = ResourceType.CowMilk;
            var requestedResourceQuantity = "5";

            var resourceRepositoryMock = new Mock<IResourceRepository>();
            var inventoryResourceRepositoryMock = new Mock<IInventoryResourceRepository>();
            var tradeRepositoryMock = new Mock<ITradeRepository>();
            var achievementServiceMock = new Mock<IAchievementService>();
            var userRepositoryMock = new Mock<IUserRepository>();

            var tradeService = new TradeService(achievementServiceMock.Object, tradeRepositoryMock.Object, inventoryResourceRepositoryMock.Object, resourceRepositoryMock.Object, userRepositoryMock.Object);

            // Act
            await tradeService.CreateTradeAsync(givenResourceType, givenResourceQuantity, requestedResourceType, requestedResourceQuantity);

            // Assert
            // Exception is expected
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task CreateTradeAsync_InvalidGivenResourceType_ThrowsException()
        {
            // Arrange
            var givenResourceType = (ResourceType)100;
            var givenResourceQuantity = "10";
            var requestedResourceType = ResourceType.CowMilk;
            var requestedResourceQuantity = "5";

            var resourceRepositoryMock = new Mock<IResourceRepository>();
            var inventoryResourceRepositoryMock = new Mock<IInventoryResourceRepository>();
            var tradeRepositoryMock = new Mock<ITradeRepository>();
            var achievementServiceMock = new Mock<IAchievementService>();
            var userRepositoryMock = new Mock<IUserRepository>();

            var tradeService = new TradeService(achievementServiceMock.Object, tradeRepositoryMock.Object, inventoryResourceRepositoryMock.Object, resourceRepositoryMock.Object, userRepositoryMock.Object);

            // Act
            await tradeService.CreateTradeAsync(givenResourceType, givenResourceQuantity, requestedResourceType, requestedResourceQuantity);

            // Assert
            // Exception is expected
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task CreateTradeAsync_InvalidGivenResourceQuantity_ThrowsException()
        {
            // Arrange
            var givenResourceType = ResourceType.ChickenEgg;
            var givenResourceQuantity = "-10"; // Negative quantity
            var requestedResourceType = ResourceType.CowMilk;
            var requestedResourceQuantity = "5";

            var resourceRepositoryMock = new Mock<IResourceRepository>();
            var inventoryResourceRepositoryMock = new Mock<IInventoryResourceRepository>();
            var tradeRepositoryMock = new Mock<ITradeRepository>();
            var achievementServiceMock = new Mock<IAchievementService>();
            var userRepositoryMock = new Mock<IUserRepository>();

            var tradeService = new TradeService(achievementServiceMock.Object, tradeRepositoryMock.Object, inventoryResourceRepositoryMock.Object, resourceRepositoryMock.Object, userRepositoryMock.Object);

            // Act
            await tradeService.CreateTradeAsync(givenResourceType, givenResourceQuantity, requestedResourceType, requestedResourceQuantity);

            // Assert
            // Exception is expected
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task CreateTradeAsync_GivenOrRequestedResourceIsWater_ThrowsException()
        {
            // Arrange
            var givenResourceType = ResourceType.Water;
            var givenResourceQuantity = "10";
            var requestedResourceType = ResourceType.CowMilk;
            var requestedResourceQuantity = "5";

            var resourceRepositoryMock = new Mock<IResourceRepository>();
            var inventoryResourceRepositoryMock = new Mock<IInventoryResourceRepository>();
            var tradeRepositoryMock = new Mock<ITradeRepository>();
            var achievementServiceMock = new Mock<IAchievementService>();
            var userRepositoryMock = new Mock<IUserRepository>();

            var tradeService = new TradeService(achievementServiceMock.Object, tradeRepositoryMock.Object, inventoryResourceRepositoryMock.Object, resourceRepositoryMock.Object, userRepositoryMock.Object);

            // Act
            await tradeService.CreateTradeAsync(givenResourceType, givenResourceQuantity, requestedResourceType, requestedResourceQuantity);

            // Assert
            // Exception is expected
        }

        [TestMethod]
        public async Task PerformTradeAsync_ValidTrade_TradeCompletedAndResourcesUpdated()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var otherUserId = Guid.NewGuid();
            var tradeId = Guid.NewGuid();
            var givenResourceId = Guid.NewGuid();
            var requestedResourceId = Guid.NewGuid();
            var givenResourceQuantity = 5;
            var requestedResourceQuantity = 10;

            var currentUser = new User(userId, "currentuser", 100, 5, 2, DateTime.Now, DateTime.Now);
            var otherUser = new User(otherUserId, "otheruser", 100, 5, 2, DateTime.Now, DateTime.Now);

            var trade = new Trade(tradeId, userId, givenResourceId, givenResourceQuantity, requestedResourceId, requestedResourceQuantity, DateTime.UtcNow, false);

            var userGivenResource = new InventoryResource(Guid.NewGuid(), userId, givenResourceId, givenResourceQuantity);
            var userRequestedResource = new InventoryResource(Guid.NewGuid(), userId, requestedResourceId, requestedResourceQuantity);
            var otherUserRequestedResource = new InventoryResource(Guid.NewGuid(), otherUserId, requestedResourceId, requestedResourceQuantity);

            var inventoryResourceRepositoryMock = new Mock<IInventoryResourceRepository>();
            inventoryResourceRepositoryMock.Setup(repo => repo.GetUserResourceByResourceIdAsync(userId, userRequestedResource.ResourceId))
                .ReturnsAsync(userRequestedResource);
            inventoryResourceRepositoryMock.Setup(repo => repo.GetUserResourceByResourceIdAsync(otherUserId, userGivenResource.ResourceId))
                .ReturnsAsync((InventoryResource)null);
            inventoryResourceRepositoryMock.Setup(repo => repo.GetUserResourceByResourceIdAsync(otherUserId, otherUserRequestedResource.ResourceId))
                .ReturnsAsync(otherUserRequestedResource);

            var tradeRepositoryMock = new Mock<ITradeRepository>();
            tradeRepositoryMock.Setup(repo => repo.GetTradeByIdAsync(tradeId))
                .ReturnsAsync(trade);

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(userId))
                .ReturnsAsync(currentUser);
            userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(otherUserId))
                .ReturnsAsync(otherUser);

            var achievementServiceMock = new Mock<IAchievementService>();
            achievementServiceMock.Setup(repo => repo.CheckTradeAchievements(otherUserId))
                .Returns(Task.CompletedTask);
            achievementServiceMock.Setup(repo => repo.CheckInventoryAchievements())
                .Returns(Task.CompletedTask);

            var tradeService = new TradeService(
                achievementServiceMock.Object,
                tradeRepositoryMock.Object,
                inventoryResourceRepositoryMock.Object,
                null,
                userRepositoryMock.Object);

            GameStateManager.SetCurrentUser(currentUser);

            // Act
            await tradeService.PerformTradeAsync(tradeId);

            // Assert
            inventoryResourceRepositoryMock.Verify(repo => repo.UpdateUserResourceAsync(userRequestedResource), Times.Once);
            inventoryResourceRepositoryMock.Verify(repo => repo.AddUserResourceAsync(It.IsAny<InventoryResource>()), Times.Once);
            inventoryResourceRepositoryMock.Verify(repo => repo.AddUserResourceAsync(It.IsAny<InventoryResource>()), Times.Once);
            tradeRepositoryMock.Verify(repo => repo.DeleteTradeAsync(tradeId), Times.Once);
            userRepositoryMock.Verify(repo => repo.UpdateUserAsync(otherUser), Times.Once);
            userRepositoryMock.Verify(repo => repo.UpdateUserAsync(currentUser), Times.Once);
            achievementServiceMock.Verify(repo => repo.CheckTradeAchievements(otherUserId), Times.Once);
            achievementServiceMock.Verify(repo => repo.CheckInventoryAchievements(), Times.Once);
        }



        [TestMethod]
        public async Task PerformTradeAsync_UserNotLoggedIn_ThrowsException()
        {
            // Arrange
            var tradeId = Guid.NewGuid();
            var inventoryResourceRepositoryMock = new Mock<IInventoryResourceRepository>();
            var tradeRepositoryMock = new Mock<ITradeRepository>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var achievementServiceMock = new Mock<IAchievementService>();

            var tradeService = new TradeService(achievementServiceMock.Object, tradeRepositoryMock.Object, inventoryResourceRepositoryMock.Object, null, userRepositoryMock.Object);
            GameStateManager.SetCurrentUser(null);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(() => tradeService.PerformTradeAsync(tradeId));
        }

        [TestMethod]
        public async Task PerformTradeAsync_TradeNotFound_ThrowsException()
        {
            // Arrange
            var tradeId = Guid.NewGuid();
            var inventoryResourceRepositoryMock = new Mock<IInventoryResourceRepository>();
            var tradeRepositoryMock = new Mock<ITradeRepository>();
            tradeRepositoryMock.Setup(repo => repo.GetTradeByIdAsync(tradeId)).ReturnsAsync((Trade)null);
            var userRepositoryMock = new Mock<IUserRepository>();
            var achievementServiceMock = new Mock<IAchievementService>();

            var tradeService = new TradeService(achievementServiceMock.Object, tradeRepositoryMock.Object, inventoryResourceRepositoryMock.Object, null, userRepositoryMock.Object);
            GameStateManager.SetCurrentUser(new User(Guid.NewGuid(), "testuser", 100, 5, 2, DateTime.Now, DateTime.Now));

            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(() => tradeService.PerformTradeAsync(tradeId));
        }

        [TestMethod]
        public async Task CancelTradeAsync_ValidTrade_TradeCanceledAndInventoryUpdated()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var tradeId = Guid.NewGuid();
            var givenResourceId = Guid.NewGuid();
            var givenResourceQuantity = 5;

            var userGivenResource = new InventoryResource(Guid.NewGuid(), userId, givenResourceId, givenResourceQuantity);
            var trade = new Trade(tradeId, userId, givenResourceId, givenResourceQuantity, Guid.NewGuid(), 10, DateTime.UtcNow, false);

            var tradeRepositoryMock = new Mock<ITradeRepository>();
            tradeRepositoryMock.Setup(repo => repo.GetTradeByIdAsync(tradeId)).ReturnsAsync(trade);

            var inventoryResourceRepositoryMock = new Mock<IInventoryResourceRepository>();
            inventoryResourceRepositoryMock.Setup(repo => repo.GetUserResourceByResourceIdAsync(userId, givenResourceId)).ReturnsAsync(userGivenResource);

            var achievementServiceMock = new Mock<IAchievementService>();
            var resourceRepositoryMock = new Mock<IResourceRepository>();
            var userRepositoryMock = new Mock<IUserRepository>();

            var tradeService = new TradeService(achievementServiceMock.Object, tradeRepositoryMock.Object, inventoryResourceRepositoryMock.Object, resourceRepositoryMock.Object, userRepositoryMock.Object);

            GameStateManager.SetCurrentUser(new User(userId, "testuser", 100, 5, 2, DateTime.Now, DateTime.Now));

            // Act
            await tradeService.CancelTradeAsync(tradeId);

            // Assert
            inventoryResourceRepositoryMock.Verify(repo => repo.UpdateUserResourceAsync(userGivenResource), Times.Once);
            tradeRepositoryMock.Verify(repo => repo.DeleteTradeAsync(tradeId), Times.Once);
            achievementServiceMock.Verify(repo => repo.CheckInventoryAchievements(), Times.Once);

        }


        [TestMethod]
        public async Task CancelTradeAsync_UserNotLoggedIn_ThrowsException()
        {
            // Arrange
            var tradeId = Guid.NewGuid();
            var tradeRepositoryMock = new Mock<ITradeRepository>();
            var inventoryResourceRepositoryMock = new Mock<IInventoryResourceRepository>();
            var achievementServiceMock = new Mock<IAchievementService>();

            var tradeService = new TradeService(null, tradeRepositoryMock.Object, inventoryResourceRepositoryMock.Object, null, null);
            GameStateManager.SetCurrentUser(null);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(async () => await tradeService.CancelTradeAsync(tradeId));
        }

        [TestMethod]
        public async Task CancelTradeAsync_TradeNotFound_ThrowsException()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var tradeId = Guid.NewGuid();
            var tradeRepositoryMock = new Mock<ITradeRepository>();
            tradeRepositoryMock.Setup(repo => repo.GetTradeByIdAsync(tradeId)).ReturnsAsync((Trade)null);

            var inventoryResourceRepositoryMock = new Mock<IInventoryResourceRepository>();
            var achievementServiceMock = new Mock<IAchievementService>();

            var tradeService = new TradeService(null, tradeRepositoryMock.Object, inventoryResourceRepositoryMock.Object, null, null);
            GameStateManager.SetCurrentUser(new User(userId, "testuser", 100, 5, 2, DateTime.Now, DateTime.Now));

            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(async () => await tradeService.CancelTradeAsync(tradeId));
        }

        [TestMethod]
        public void GetPicturePathByResourceType_ReturnsCorrectPaths()
        {
            // Arrange
            var tradeService = new TradeService(null, null, null, null, null);

            // Act
            var carrotPath = tradeService.GetPicturePathByResourceType(ResourceType.Carrot);
            var cornPath = tradeService.GetPicturePathByResourceType(ResourceType.Corn);
            var wheatPath = tradeService.GetPicturePathByResourceType(ResourceType.Wheat);
            var tomatoPath = tradeService.GetPicturePathByResourceType(ResourceType.Tomato);
            var chickenMeatPath = tradeService.GetPicturePathByResourceType(ResourceType.ChickenMeat);
            var duckMeatPath = tradeService.GetPicturePathByResourceType(ResourceType.DuckMeat);
            var muttonPath = tradeService.GetPicturePathByResourceType(ResourceType.Mutton);
            var sheepWoolPath = tradeService.GetPicturePathByResourceType(ResourceType.SheepWool);
            var chickenEggPath = tradeService.GetPicturePathByResourceType(ResourceType.ChickenEgg);
            var duckEggPath = tradeService.GetPicturePathByResourceType(ResourceType.DuckEgg);
            var cowMilkPath = tradeService.GetPicturePathByResourceType(ResourceType.CowMilk);
            var cowPath = tradeService.GetPicturePathByResourceType(ResourceType.Steak);

            // Assert
            Assert.AreEqual(Constants.CarrotPath, carrotPath);
            Assert.AreEqual(Constants.CornPath, cornPath);
            Assert.AreEqual(Constants.WheatPath, wheatPath);
            Assert.AreEqual(Constants.TomatoPath, tomatoPath);
            Assert.AreEqual(Constants.ChickenPath, chickenMeatPath);
            Assert.AreEqual(Constants.DuckPath, duckMeatPath);
            Assert.AreEqual(Constants.SheepPath, muttonPath);
            Assert.AreEqual(Constants.WoolPath, sheepWoolPath);
            Assert.AreEqual(Constants.ChickenEggPath, chickenEggPath);
            Assert.AreEqual(Constants.DuckEggPath, duckEggPath);
            Assert.AreEqual(Constants.MilkPath, cowMilkPath);
            Assert.AreEqual(Constants.CowPath, cowPath);
        }

    }

}
