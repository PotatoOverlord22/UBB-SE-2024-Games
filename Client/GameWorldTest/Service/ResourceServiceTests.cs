using GameWorld.Entities;
using GameWorld.Repositories;
using Moq;

namespace GameWorld.Services.Tests
{
    [TestClass]
    public class ResourceServiceTests
    {
        [TestMethod]
        public async Task GetResourceByIdAsync_ValidId_ReturnsResource()
        {
            // Arrange
            var resourceId = Guid.NewGuid();
            var expectedResource = new Resource(resourceId, ResourceType.Water);

            var resourceRepositoryMock = new Mock<IResourceRepository>();
            resourceRepositoryMock.Setup(repo => repo.GetResourceByIdAsync(resourceId)).ReturnsAsync(expectedResource);

            var resourceService = new ResourceService(resourceRepositoryMock.Object);

            // Act
            var result = await resourceService.GetResourceByIdAsync(resourceId);

            // Assert
            Assert.AreEqual(expectedResource, result);
        }

        [TestMethod]
        public async Task GetResourceByIdAsync_InvalidId_ReturnsNull()
        {
            // Arrange
            var resourceId = Guid.NewGuid();

            var resourceRepositoryMock = new Mock<IResourceRepository>();
            resourceRepositoryMock.Setup(repo => repo.GetResourceByIdAsync(resourceId)).ReturnsAsync((Resource)null);

            var resourceService = new ResourceService(resourceRepositoryMock.Object);

            // Act
            var result = await resourceService.GetResourceByIdAsync(resourceId);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task GetAllResourcesAsync_ReturnsListOfResources()
        {
            // Arrange
            var expectedResources = new List<Resource>
            {
                new Resource(Guid.NewGuid(), ResourceType.Water),
                new Resource(Guid.NewGuid(), ResourceType.Corn)
            };

            var resourceRepositoryMock = new Mock<IResourceRepository>();
            resourceRepositoryMock.Setup(repo => repo.GetAllResourcesAsync()).ReturnsAsync(expectedResources);

            var resourceService = new ResourceService(resourceRepositoryMock.Object);

            // Act
            var result = await resourceService.GetAllResourcesAsync();

            // Assert
            CollectionAssert.AreEqual(expectedResources, result);
        }

        [TestMethod]
        public async Task GetAllResourcesAsync_EmptyList_ReturnsEmptyList()
        {
            // Arrange
            var expectedResources = new List<Resource>();

            var resourceRepositoryMock = new Mock<IResourceRepository>();
            resourceRepositoryMock.Setup(repo => repo.GetAllResourcesAsync()).ReturnsAsync(expectedResources);

            var resourceService = new ResourceService(resourceRepositoryMock.Object);

            // Act
            var result = await resourceService.GetAllResourcesAsync();

            // Assert
            CollectionAssert.AreEqual(expectedResources, result);
        }
    }
}
