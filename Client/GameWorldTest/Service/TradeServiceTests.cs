using GameWorld.Models;
using GameWorld.Repositories;
using GameWorld.Resources.Utils;
using GameWorld.Services.Interfaces;
using Moq;

namespace GameWorld.Services.Tests
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
        public async Task CreateTradeAsync_UserNotLoggedIn_ThrowsException()
        {
            // Arrange
            var tradeService = new TradeService(null, null, null, null, null);
            GameStateManager.SetCurrentUser(null);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(() =>
                tradeService.CreateTradeAsync(ResourceType.Wheat, "5", ResourceType.Corn, "10"),
                "User must be logged in!");
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
        public async Task CreateTradeAsync_ResourceTypeIsWater_ThrowsException()
        {
            // Arrange
            var givenResourceType = ResourceType.Wheat;
            var givenResourceQuantity = "5";
            var requestedResourceType = ResourceType.Water;
            var requestedResourceQuantity = "10";

            var resourceRepositoryMock = new Mock<IResourceRepository>();
            resourceRepositoryMock.Setup(repo => repo.GetResourceByTypeAsync(ResourceType.Wheat))
                .ReturnsAsync(new Resource(Guid.NewGuid(), ResourceType.Wheat));
            resourceRepositoryMock.Setup(repo => repo.GetResourceByTypeAsync(ResourceType.Water))
                .ReturnsAsync(new Resource(Guid.NewGuid(), ResourceType.Water));

            var inventoryResourceRepositoryMock = new Mock<IInventoryResourceRepository>();
            inventoryResourceRepositoryMock.Setup(repo => repo.GetUserResourceByResourceIdAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(new InventoryResource(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), 10)); // Mock a user having enough resources

            var tradeService = new TradeService(null, null, inventoryResourceRepositoryMock.Object, resourceRepositoryMock.Object, null);
            GameStateManager.SetCurrentUser(new User(Guid.NewGuid(), "username", 100, 5, 2, DateTime.Now, DateTime.Now));

            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(() =>
                tradeService.CreateTradeAsync(givenResourceType, givenResourceQuantity, requestedResourceType, requestedResourceQuantity),
                $"Select the resources to give and get!");
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

            // Mock current user
            var currentUser = new User(userId, "currentuser", 100, 5, 2, DateTime.Now, DateTime.Now);
            GameStateManager.SetCurrentUser(currentUser);

            // Mock other user
            var otherUser = new User(otherUserId, "otheruser", 100, 5, 2, DateTime.Now, DateTime.Now);

            // Mock trade
            var trade = new Trade(tradeId, userId, givenResourceId, givenResourceQuantity, requestedResourceId, requestedResourceQuantity, DateTime.UtcNow, false);

            // Mock user resources
            var userGivenResource = new InventoryResource(Guid.NewGuid(), userId, givenResourceId, givenResourceQuantity);
            var userRequestedResource = new InventoryResource(Guid.NewGuid(), userId, requestedResourceId, requestedResourceQuantity);
            var initialRequestedResource = new InventoryResource(Guid.NewGuid(), trade.UserId, trade.ResourceToGetResourceId, trade.ResourceToGetQuantity);

            // Mock repositories
            var inventoryResourceRepositoryMock = new Mock<IInventoryResourceRepository>();
            inventoryResourceRepositoryMock.Setup(repo => repo.GetUserResourceByResourceIdAsync(userId, requestedResourceId))
                .ReturnsAsync(userRequestedResource);
            inventoryResourceRepositoryMock.Setup(repo => repo.GetUserResourceByResourceIdAsync(userId, givenResourceId))
                .ReturnsAsync(userGivenResource);
            inventoryResourceRepositoryMock.Setup(repo => repo.GetUserResourceByResourceIdAsync(trade.UserId, trade.ResourceToGetResourceId))
                .ReturnsAsync(initialRequestedResource);

            var tradeRepositoryMock = new Mock<ITradeRepository>();
            tradeRepositoryMock.Setup(repo => repo.GetTradeByIdAsync(tradeId))
                .ReturnsAsync(trade);

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(userId))
                .ReturnsAsync(currentUser);
            userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(otherUserId))
                .ReturnsAsync(otherUser);

            var achievementServiceMock = new Mock<IAchievementService>();

            var resourceRepositoryMock = new Mock<IResourceRepository>();
            resourceRepositoryMock.Setup(repo => repo.GetResourceByIdAsync(trade.ResourceToGetResourceId))
                .ReturnsAsync(new Resource(trade.ResourceToGetResourceId, ResourceType.Wheat));

            var tradeService = new TradeService(
                achievementServiceMock.Object,
                tradeRepositoryMock.Object,
                inventoryResourceRepositoryMock.Object,
                resourceRepositoryMock.Object,
                userRepositoryMock.Object);

            // Act
            await tradeService.PerformTradeAsync(tradeId);

            // Assert

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

        [TestMethod]
        [ExpectedException(typeof(Exception), "User must be logged in!")]
        public async Task GetAllTradesExceptCreatedByLoggedUser_UserNotLoggedIn_ThrowsException()
        {
            // Arrange
            var tradeService = new TradeService(null, null, null, null, null);
            GameStateManager.SetCurrentUser(null); // Simulate user not logged in

            // Act
            await tradeService.GetAllTradesExceptCreatedByLoggedUser();

            // Assert
            // Expects an exception to be thrown
        }


        [TestMethod]
        [ExpectedException(typeof(Exception), "Requested resource was not found in the database!")]
        public async Task CreateTradeAsync_RequestedResourceNotFound_ThrowsException()
        {
            // Arrange
            var resourceRepositoryMock = new Mock<IResourceRepository>();
            resourceRepositoryMock.Setup(repo => repo.GetResourceByTypeAsync(It.IsAny<ResourceType>()))
                .ReturnsAsync((Resource)null);

            var tradeService = new TradeService(null, null, null, resourceRepositoryMock.Object, null);
            GameStateManager.SetCurrentUser(new User(Guid.NewGuid(), "username", 100, 5, 2, DateTime.Now, DateTime.Now));

            // Act
            await tradeService.CreateTradeAsync(ResourceType.Carrot, "5", ResourceType.Wheat, "10");

            // Assert
            // Expects an exception to be thrown
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "You don't have that ammount of Carrot!")]
        public async Task CreateTradeAsync_UserDoesNotHaveEnoughResource_ThrowsException()
        {
            // Arrange
            var resourceRepositoryMock = new Mock<IResourceRepository>();
            resourceRepositoryMock.Setup(repo => repo.GetResourceByTypeAsync(ResourceType.Carrot))
                .ReturnsAsync(new Resource(Guid.NewGuid(), ResourceType.Carrot));

            var inventoryResourceRepositoryMock = new Mock<IInventoryResourceRepository>();
            inventoryResourceRepositoryMock.Setup(repo => repo.GetUserResourceByResourceIdAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync((InventoryResource)null);

            var tradeService = new TradeService(null, null, inventoryResourceRepositoryMock.Object, resourceRepositoryMock.Object, null);
            GameStateManager.SetCurrentUser(new User(Guid.NewGuid(), "username", 100, 5, 2, DateTime.Now, DateTime.Now));

            // Act
            await tradeService.CreateTradeAsync(ResourceType.Carrot, "5", ResourceType.Wheat, "10");

            // Assert
            // Expects an exception to be thrown
        }

        [TestMethod]
        public async Task CreateTradeAsync_NonPositiveQuantities_ThrowsException()
        {
            // Arrange
            var givenResourceType = ResourceType.Corn;
            var givenResourceQuantity = "-5";
            var requestedResourceType = ResourceType.Wheat;
            var requestedResourceQuantity = "0";

            var resourceRepositoryMock = new Mock<IResourceRepository>();
            resourceRepositoryMock.Setup(repo => repo.GetResourceByTypeAsync(It.IsAny<ResourceType>()))
                .ReturnsAsync(new Resource(Guid.NewGuid(), ResourceType.Corn));

            var inventoryResourceRepositoryMock = new Mock<IInventoryResourceRepository>();
            inventoryResourceRepositoryMock.Setup(repo => repo.GetUserResourceByResourceIdAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(new InventoryResource(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), 10)); // Mock a user having enough resources

            var tradeService = new TradeService(null, null, inventoryResourceRepositoryMock.Object, resourceRepositoryMock.Object, null);
            GameStateManager.SetCurrentUser(new User(Guid.NewGuid(), "username", 100, 5, 2, DateTime.Now, DateTime.Now));

            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(() =>
                tradeService.CreateTradeAsync(givenResourceType, givenResourceQuantity, requestedResourceType, requestedResourceQuantity),
                $"Input should be a positive integer!");
        }


        [TestMethod]
        [ExpectedException(typeof(Exception), "Select the resources to give and get!")]
        public async Task CreateTradeAsync_WaterResourceSelected_ThrowsException()
        {
            // Arrange
            var resourceRepositoryMock = new Mock<IResourceRepository>();
            var inventoryResourceRepositoryMock = new Mock<IInventoryResourceRepository>();
            var tradeRepositoryMock = new Mock<ITradeRepository>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var achievementServiceMock = new Mock<IAchievementService>();

            var tradeService = new TradeService(achievementServiceMock.Object, tradeRepositoryMock.Object, inventoryResourceRepositoryMock.Object, resourceRepositoryMock.Object, userRepositoryMock.Object);

            // Act
            await tradeService.CreateTradeAsync(ResourceType.Water, "5", ResourceType.Wheat, "10");

            // Assert
            // Expects an exception to be thrown
        }

        [TestMethod]
        public async Task CancelTradeAsync_UserDoesNotHaveResource_ResourceAddedToInventory()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var tradeId = Guid.NewGuid();
            var givenResourceId = Guid.NewGuid();
            var givenResourceQuantity = 5;

            var currentUser = new User(userId, "currentuser", 100, 5, 2, DateTime.Now, DateTime.Now);
            var trade = new Trade(tradeId, userId, givenResourceId, givenResourceQuantity, Guid.NewGuid(), 10, DateTime.UtcNow, false);

            var userGivenResource = new InventoryResource(Guid.NewGuid(), userId, givenResourceId, givenResourceQuantity);

            var inventoryResourceRepositoryMock = new Mock<IInventoryResourceRepository>();
            inventoryResourceRepositoryMock.Setup(repo => repo.GetUserResourceByResourceIdAsync(userId, trade.ResourceToGiveId))
                .ReturnsAsync((InventoryResource)null);

            var tradeRepositoryMock = new Mock<ITradeRepository>();
            tradeRepositoryMock.Setup(repo => repo.GetTradeByIdAsync(tradeId))
                .ReturnsAsync(trade);

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(userId))
                .ReturnsAsync(currentUser);

            var achievementServiceMock = new Mock<IAchievementService>();

            var tradeService = new TradeService(
                achievementServiceMock.Object,
                tradeRepositoryMock.Object,
                inventoryResourceRepositoryMock.Object,
                null,
                userRepositoryMock.Object);

            GameStateManager.SetCurrentUser(currentUser);

            // Act
            await tradeService.CancelTradeAsync(tradeId);

            // Assert
            inventoryResourceRepositoryMock.Verify(repo => repo.AddUserResourceAsync(
                It.Is<InventoryResource>(r => r.OwnerId == userId &&
                                                r.ResourceId == givenResourceId &&
                                                r.Quantity == givenResourceQuantity)), Times.Once);
        }

        [TestMethod]
        public async Task CreateTradeAsync_UserGivenResourceIsNull_ThrowsException()
        {
            // Arrange
            var givenResourceType = ResourceType.Corn;
            var givenResourceQuantity = "5";
            var requestedResourceType = ResourceType.Wheat;
            var requestedResourceQuantity = "10";

            var resourceRepositoryMock = new Mock<IResourceRepository>();
            resourceRepositoryMock.Setup(repo => repo.GetResourceByTypeAsync(It.IsAny<ResourceType>()))
                .ReturnsAsync(new Resource(Guid.NewGuid(), ResourceType.Corn));

            var inventoryResourceRepositoryMock = new Mock<IInventoryResourceRepository>();
            inventoryResourceRepositoryMock.Setup(repo => repo.GetUserResourceByResourceIdAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync((InventoryResource)null);

            var tradeService = new TradeService(null, null, inventoryResourceRepositoryMock.Object, resourceRepositoryMock.Object, null);
            GameStateManager.SetCurrentUser(new User(Guid.NewGuid(), "username", 100, 5, 2, DateTime.Now, DateTime.Now));

            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(() =>
                tradeService.CreateTradeAsync(givenResourceType, givenResourceQuantity, requestedResourceType, requestedResourceQuantity),
                $"You don't have that amount of {givenResourceType.ToString()}!");
        }

        [TestMethod]
        public async Task CancelTradeAsync_UserNotLoggedIn_ThrowsException()
        {
            // Arrange
            var tradeId = Guid.NewGuid();
            GameStateManager.SetCurrentUser(null); // Simulate user not logged in

            var tradeRepositoryMock = new Mock<ITradeRepository>();
            tradeRepositoryMock.Setup(repo => repo.GetTradeByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Trade(tradeId, Guid.NewGuid(), Guid.NewGuid(), 5, Guid.NewGuid(), 10, DateTime.UtcNow, false));

            var inventoryResourceRepositoryMock = new Mock<IInventoryResourceRepository>();
            var achievementServiceMock = new Mock<IAchievementService>();

            var tradeService = new TradeService(
                achievementServiceMock.Object,
                tradeRepositoryMock.Object,
                inventoryResourceRepositoryMock.Object,
                null,
                null);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(() =>
                tradeService.CancelTradeAsync(tradeId),
                "User must be logged in!");
        }

        [TestMethod]
        public async Task PerformTradeAsync_UserNotLoggedIn_ThrowsException()
        {
            // Arrange
            var tradeId = Guid.NewGuid();
            GameStateManager.SetCurrentUser(null); // Simulate user not logged in

            var tradeRepositoryMock = new Mock<ITradeRepository>();
            tradeRepositoryMock.Setup(repo => repo.GetTradeByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Trade(tradeId, Guid.NewGuid(), Guid.NewGuid(), 5, Guid.NewGuid(), 10, DateTime.UtcNow, false));

            var inventoryResourceRepositoryMock = new Mock<IInventoryResourceRepository>();
            inventoryResourceRepositoryMock.Setup(repo => repo.GetUserResourceByResourceIdAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync((InventoryResource)null); // Simulate user doesn't have requested resource

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((User)null); // Simulate other user not found

            var achievementServiceMock = new Mock<IAchievementService>();

            var tradeService = new TradeService(
                achievementServiceMock.Object,
                tradeRepositoryMock.Object,
                inventoryResourceRepositoryMock.Object,
                null, // Pass null for IResourceRepository
                userRepositoryMock.Object); // Pass userRepositoryMock

            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(() =>
                tradeService.PerformTradeAsync(tradeId),
                "User must be logged in!");
        }

        [TestMethod]
        public async Task PerformTradeAsync_RequestedResourceNotFound_ThrowsException()
        {
            // Arrange
            var tradeId = Guid.NewGuid();
            var requestedResourceId = Guid.NewGuid();

            var currentUser = new User(
                id: Guid.NewGuid(),
                username: "testUser",
                coins: 0,
                nrItemsBought: 0,
                nrTradesPerformed: 0,
                tradeHallUnlockTime: null,
                lastTimeReceivedWater: null); // Nullable DateTime parameters

            GameStateManager.SetCurrentUser(currentUser);

            var trade = new Trade(tradeId, currentUser.Id, requestedResourceId, 5, Guid.NewGuid(), 10, DateTime.UtcNow, false);

            var tradeRepositoryMock = new Mock<ITradeRepository>();
            tradeRepositoryMock.Setup(repo => repo.GetTradeByIdAsync(tradeId)).ReturnsAsync(trade);

            var resourceRepositoryMock = new Mock<IResourceRepository>();
            resourceRepositoryMock.Setup(repo => repo.GetResourceByIdAsync(requestedResourceId)).ReturnsAsync((Resource)null);

            var inventoryResourceRepositoryMock = new Mock<IInventoryResourceRepository>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var achievementServiceMock = new Mock<IAchievementService>();

            var tradeService = new TradeService(
                achievementServiceMock.Object,
                tradeRepositoryMock.Object,
                inventoryResourceRepositoryMock.Object,
                resourceRepositoryMock.Object,
                userRepositoryMock.Object);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(() =>
                tradeService.PerformTradeAsync(tradeId),
                "Requested resource not found in the database!");
        }

        [TestMethod]
        public async Task PerformTradeAsync_UserDoesNotHaveEnoughResource_ThrowsException()
        {
            // Arrange
            var tradeId = Guid.NewGuid();
            var currentUser = new User(
                id: Guid.NewGuid(),
                username: "testUser",
                coins: 0,
                nrItemsBought: 0,
                nrTradesPerformed: 0,
                tradeHallUnlockTime: null,
                lastTimeReceivedWater: null);

            var requestedResourceId = Guid.NewGuid();
            var givenResourceId = Guid.NewGuid();
            var requestedResourceQuantity = 5;
            var givenResourceQuantity = 10;

            GameStateManager.SetCurrentUser(currentUser);

            var requestedResource = new Resource(requestedResourceId, ResourceType.Wheat);
            var givenResource = new Resource(givenResourceId, ResourceType.Corn);

            var trade = new Trade(tradeId, currentUser.Id, requestedResourceId, requestedResourceQuantity, givenResourceId, givenResourceQuantity, DateTime.UtcNow, false);

            var tradeRepositoryMock = new Mock<ITradeRepository>();
            tradeRepositoryMock.Setup(repo => repo.GetTradeByIdAsync(tradeId)).ReturnsAsync(trade);

            var resourceRepositoryMock = new Mock<IResourceRepository>();
            resourceRepositoryMock.Setup(repo => repo.GetResourceByIdAsync(requestedResourceId)).ReturnsAsync(requestedResource);
            resourceRepositoryMock.Setup(repo => repo.GetResourceByIdAsync(givenResourceId)).ReturnsAsync(givenResource);

            var inventoryResourceRepositoryMock = new Mock<IInventoryResourceRepository>();
            inventoryResourceRepositoryMock.Setup(repo => repo.GetUserResourceByResourceIdAsync(currentUser.Id, requestedResourceId))
                                            .ReturnsAsync(new InventoryResource(Guid.NewGuid(), currentUser.Id, requestedResourceId, 4)); // User has only 4 wheat, but trade requests 5

            var userRepositoryMock = new Mock<IUserRepository>();
            var achievementServiceMock = new Mock<IAchievementService>();

            var tradeService = new TradeService(
                achievementServiceMock.Object,
                tradeRepositoryMock.Object,
                inventoryResourceRepositoryMock.Object,
                resourceRepositoryMock.Object,
                userRepositoryMock.Object);

            // Act & Assert
            var exception = await Assert.ThrowsExceptionAsync<Exception>(() =>
                tradeService.PerformTradeAsync(tradeId));
            Assert.AreEqual($"You don't have that amount of {givenResource.ResourceType}!", exception.Message);
        }


        [TestMethod]
        public async Task PerformTradeAsync_UserNotFoundInDatabase()
        {
            // Arrange
            var achievementServiceMock = new Mock<IAchievementService>();
            var tradeRepositoryMock = new Mock<ITradeRepository>();
            var inventoryResourceRepositoryMock = new Mock<IInventoryResourceRepository>();
            var resourceRepositoryMock = new Mock<IResourceRepository>();
            var userRepositoryMock = new Mock<IUserRepository>();

            var tradeService = new TradeService(
                achievementServiceMock.Object,
                tradeRepositoryMock.Object,
                inventoryResourceRepositoryMock.Object,
                resourceRepositoryMock.Object,
                userRepositoryMock.Object);

            // Mock GameStateManager.GetCurrentUserId() to return a valid user ID
            GameStateManager.SetCurrentUser(new User(Guid.NewGuid(), "TestUser", 100, 0, 0, null, null));

            // Mock data
            var tradeId = Guid.NewGuid();
            var tradeUserId = Guid.NewGuid(); // User ID who created the trade
            var trade = new Trade(
                id: tradeId,
                userId: tradeUserId,
                givenResourceId: Guid.NewGuid(),
                givenResourceQuantity: 5,
                requestedResourceId: Guid.NewGuid(),
                requestedResourceQuantity: 10,
                createdTime: DateTime.UtcNow,
                isCompleted: false);

            // Mock repository methods
            tradeRepositoryMock.Setup(repo => repo.GetTradeByIdAsync(tradeId)).ReturnsAsync(trade);
            userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(tradeUserId)).ReturnsAsync((User)null);

            // Act and Assert
            await Assert.ThrowsExceptionAsync<Exception>(async () => await tradeService.PerformTradeAsync(tradeId), "The user that created the trade with cannot be found in the database.");
        }

        [TestMethod]
        public async Task PerformTradeAsync_UserThatCreatedTradeNotFound()
        {
            // Arrange
            var achievementServiceMock = new Mock<IAchievementService>();
            var tradeRepositoryMock = new Mock<ITradeRepository>();
            var inventoryResourceRepositoryMock = new Mock<IInventoryResourceRepository>();
            var resourceRepositoryMock = new Mock<IResourceRepository>();
            var userRepositoryMock = new Mock<IUserRepository>();

            var tradeService = new TradeService(
                achievementServiceMock.Object,
                tradeRepositoryMock.Object,
                inventoryResourceRepositoryMock.Object,
                resourceRepositoryMock.Object,
                userRepositoryMock.Object);

            // Mock GameStateManager.GetCurrentUserId() to return a valid user ID
            GameStateManager.SetCurrentUser(new User(Guid.NewGuid(), "TestUser", 100, 0, 0, null, null));

            // Mock data
            var tradeId = Guid.NewGuid();
            var trade = new Trade(
                id: tradeId,
                userId: Guid.NewGuid(), // User ID who created the trade
                givenResourceId: Guid.NewGuid(),
                givenResourceQuantity: 5,
                requestedResourceId: Guid.NewGuid(),
                requestedResourceQuantity: 10,
                createdTime: DateTime.UtcNow,
                isCompleted: false);

            // Mock repository methods
            tradeRepositoryMock.Setup(repo => repo.GetTradeByIdAsync(tradeId)).ReturnsAsync(trade);
            userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(trade.UserId)).ReturnsAsync((User)null); // User not found in the database

            // Act and Assert
            await Assert.ThrowsExceptionAsync<Exception>(async () => await tradeService.PerformTradeAsync(tradeId), "The user that created the trade with cannot be found in the database.");
        }


    }

}
