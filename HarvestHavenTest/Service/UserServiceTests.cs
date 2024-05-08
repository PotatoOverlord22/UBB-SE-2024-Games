using HarvestHaven.Entities;
using HarvestHaven.Repositories;
using HarvestHaven.Services;
using HarvestHaven.Utils;
using Moq;

namespace HarvestHaven.Tests.Services
{
    [TestClass]
    public class UserServiceTests
    {
        private User currentUser;

        [TestInitialize]
        public void Setup()
        {
            currentUser = new User(Guid.NewGuid(), "TestUser", 100, 5, 2, DateTime.Now, DateTime.Now);
            GameStateManager.SetCurrentUser(currentUser);
        }

        [TestMethod]
        public async Task GetUserByIdAsync_WhenValidUserId_ReturnsUser()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var expectedUser = new User(userId, "TestUser", 100, 5, 2, DateTime.Now, DateTime.Now);
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(userId)).ReturnsAsync(expectedUser);
            var userService = new UserService(userRepositoryMock.Object, null, null, null);

            // Act
            var result = await userService.GetUserByIdAsync(userId);

            // Assert
            Assert.AreEqual(expectedUser, result);
        }

        [TestMethod]
        public async Task GetInventoryResources_WhenCurrentUserIsNotNull_ReturnsDictionary()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User(userId, "TestUser", 100, 5, 2, DateTime.Now, DateTime.Now);
            Guid resource1Id = Guid.NewGuid();
            Guid resource2Id = Guid.NewGuid();
            var inventoryResources = new List<InventoryResource>
            {
                new InventoryResource(Guid.NewGuid(), userId, resource1Id, 10),
                new InventoryResource(Guid.NewGuid(), userId, resource2Id, 5)
            };
            var resources = new List<Resource>
            {
                new Resource(resource1Id, ResourceType.Steak),
                new Resource(resource2Id, ResourceType.SheepWool)
            };

            var inventoryResourceRepositoryMock = new Mock<IInventoryResourceRepository>();
            inventoryResourceRepositoryMock.Setup(repo => repo.GetUserResourcesAsync(userId)).ReturnsAsync(inventoryResources);

            var resourceRepositoryMock = new Mock<IResourceRepository>();
            resourceRepositoryMock.Setup(repo => repo.GetResourceByIdAsync(inventoryResources[0].ResourceId)).ReturnsAsync(resources[0]);
            resourceRepositoryMock.Setup(repo => repo.GetResourceByIdAsync(inventoryResources[1].ResourceId)).ReturnsAsync(resources[1]);

            var userService = new UserService(null, inventoryResourceRepositoryMock.Object, resourceRepositoryMock.Object, null);

            // Act
            var result = await userService.GetInventoryResources(userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(inventoryResources.Count, result.Count);
            foreach (var inventoryResource in inventoryResources)
            {
                Assert.IsTrue(result.ContainsKey(inventoryResource));
            }
        }
    }
}
