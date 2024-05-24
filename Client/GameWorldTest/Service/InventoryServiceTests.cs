using GameWorld.Entities;
using GameWorld.Models;
using GameWorld.Resources.Utils;
using GameWorld.Services;
using Moq;

namespace GameWorld.Services.Tests
{
    [TestClass]
    public class InventoryServiceTests
    {
        [TestMethod]
        public async Task GetCorrespondingValueForLabel_AllLabels_ReturnsCorrespondingQuantities()
        {
            // Arrange
            var userId = new Guid();
            GameStateManager.SetCurrentUser(new User(userId, "test", 100, 100, 100, null, null));
            var userServiceMock = new Mock<IUserService>();
            var expectedQuantity = "12";
            var wheatResourceId = new Guid();
            var tomatoResourceId = new Guid();
            var chickenResourceId = new Guid();
            var carrotResourceId = new Guid();
            var cornResourceId = new Guid();
            var sheepResourceId = new Guid();
            var chickenEggResourceId = new Guid();
            var woolResourceId = new Guid();
            var milkResourceId = new Guid();
            var duckEggResourceId = new Guid();
            var cowResourceId = new Guid();
            var duckResourceId = new Guid();
            var inventoryResources = new Dictionary<InventoryResource, Resource>
            {
                { new InventoryResource(new Guid(), userId, wheatResourceId, 12), new Resource(wheatResourceId, ResourceType.Wheat) },
                { new InventoryResource(new Guid(), userId, tomatoResourceId, 12), new Resource(tomatoResourceId, ResourceType.Tomato) },
                { new InventoryResource(new Guid(), userId, chickenResourceId, 12), new Resource(chickenResourceId, ResourceType.ChickenMeat) },
                { new InventoryResource(new Guid(), userId, carrotResourceId, 12), new Resource(carrotResourceId, ResourceType.Carrot) },
                { new InventoryResource(new Guid(), userId, cornResourceId, 12), new Resource(cornResourceId, ResourceType.Corn) },
                { new InventoryResource(new Guid(), userId, sheepResourceId, 12), new Resource(sheepResourceId, ResourceType.Mutton) },
                { new InventoryResource(new Guid(), userId, chickenEggResourceId, 12), new Resource(chickenEggResourceId, ResourceType.ChickenEgg) },
                { new InventoryResource(new Guid(), userId, woolResourceId, 12), new Resource(woolResourceId, ResourceType.SheepWool) },
                { new InventoryResource(new Guid(), userId, milkResourceId, 12), new Resource(milkResourceId, ResourceType.CowMilk) },
                { new InventoryResource(new Guid(), userId, duckResourceId, 12), new Resource(duckResourceId, ResourceType.DuckMeat) },
                { new InventoryResource(new Guid(), userId, duckEggResourceId, 12), new Resource(duckEggResourceId, ResourceType.DuckEgg) },
                { new InventoryResource(new Guid(), userId, cowResourceId, 12), new Resource(cowResourceId, ResourceType.Steak) },
            };
            userServiceMock.Setup(service => service.GetInventoryResources(userId)).ReturnsAsync(inventoryResources);

            var inventoryService = new InventoryService(userServiceMock.Object);

            // Act
            string wheatResult = await inventoryService.GetCorrespondingValueForLabel("wheatLabel");
            string tomatoResult = await inventoryService.GetCorrespondingValueForLabel("tomatoLabel");
            string chickenResult = await inventoryService.GetCorrespondingValueForLabel("chickenLabel");
            string carrotResult = await inventoryService.GetCorrespondingValueForLabel("carrotLabel");
            string cornResult = await inventoryService.GetCorrespondingValueForLabel("cornLabel");
            string sheepResult = await inventoryService.GetCorrespondingValueForLabel("sheepLabel");
            string chickenEggResult = await inventoryService.GetCorrespondingValueForLabel("chickenEggLabel");
            string woolResult = await inventoryService.GetCorrespondingValueForLabel("woolLabel");
            string milkResult = await inventoryService.GetCorrespondingValueForLabel("milkLabel");
            string duckEggResult = await inventoryService.GetCorrespondingValueForLabel("duckEggLabel");
            string cowResult = await inventoryService.GetCorrespondingValueForLabel("cowLabel");
            string duckResult = await inventoryService.GetCorrespondingValueForLabel("duckLabel");

            // Assert
            Assert.AreEqual(expectedQuantity, wheatResult);
            Assert.AreEqual(expectedQuantity, tomatoResult);
            Assert.AreEqual(expectedQuantity, chickenResult);
            Assert.AreEqual(expectedQuantity, carrotResult);
            Assert.AreEqual(expectedQuantity, cornResult);
            Assert.AreEqual(expectedQuantity, sheepResult);
            Assert.AreEqual(expectedQuantity, chickenEggResult);
            Assert.AreEqual(expectedQuantity, woolResult);
            Assert.AreEqual(expectedQuantity, milkResult);
            Assert.AreEqual(expectedQuantity, duckEggResult);
            Assert.AreEqual(expectedQuantity, cowResult);
            Assert.AreEqual(expectedQuantity, duckResult);
        }


        [TestMethod]
        public async Task GetCorrespondingValueForLabel_ResourceNotFound_ReturnsZero()
        {
            // Arrange
            var userId = new Guid();
            GameStateManager.SetCurrentUser(new User(userId, "test", 100, 100, 100, null, null));
            var userServiceMock = new Mock<IUserService>();
            var inventoryResources = new Dictionary<InventoryResource, Resource>();
            userServiceMock.Setup(service => service.GetInventoryResources(userId)).ReturnsAsync(inventoryResources);

            var inventoryService = new InventoryService(userServiceMock.Object);

            // Act
            var result = await inventoryService.GetCorrespondingValueForLabel("carrotLabel");

            // Assert
            Assert.AreEqual("0", result);
        }

        [TestMethod]
        public async Task GetCorrespondingValueForLabel_UnknownLabel_ReturnsZero()
        {
            // Arrange
            var userId = new Guid();
            GameStateManager.SetCurrentUser(new User(userId, "test", 100, 100, 100, null, null));
            var userServiceMock = new Mock<IUserService>();
            var inventoryResources = new Dictionary<InventoryResource, Resource>();
            userServiceMock.Setup(service => service.GetInventoryResources(userId)).ReturnsAsync(inventoryResources);

            var inventoryService = new InventoryService(userServiceMock.Object);

            // Act
            var result = await inventoryService.GetCorrespondingValueForLabel("unknownLabel");

            // Assert
            Assert.AreEqual("0", result);
        }
    }
}
