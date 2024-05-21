using GameWorld.Entities;
using GameWorld.Utils;
using Moq;
using System.Data;

namespace GameWorld.Repositories.Tests
{
    [TestClass]
    public class InventoryResourceRepositoryTests
    {
        private Mock<IDatabaseProvider> mockDatabaseProvider;
        private InventoryResourceRepository repository;
        private Mock<IDataReader> mockDataReader;

        [TestInitialize]
        public void Initialize()
        {
            mockDatabaseProvider = new Mock<IDatabaseProvider>();
            repository = new InventoryResourceRepository(mockDatabaseProvider.Object);
            mockDataReader = new Mock<IDataReader>();
        }

        [TestMethod]
        public async Task GetUserResourcesAsync_WhenCalled_ReturnsAllResources()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            var expectedResources = new List<InventoryResource>
            {
                new InventoryResource(Guid.NewGuid(), userId, Guid.NewGuid(), 10),
                new InventoryResource(Guid.NewGuid(), userId, Guid.NewGuid(), 20)
            };

            SetupMockReaderForResources(expectedResources);
            var parameters = new Dictionary<string, object> { { "@UserId", userId } };
            mockDatabaseProvider.Setup(m => m.ExecuteReaderAsync("SELECT * FROM InventoryResources WHERE UserId = @UserId", parameters))
                                .ReturnsAsync(mockDataReader.Object);

            // Act
            var result = await repository.GetUserResourcesAsync(userId);

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(expectedResources[0].Quantity, result[0].Quantity);
        }

        private void SetupMockReaderForResources(List<InventoryResource> resources)
        {
            var queue = new Queue<InventoryResource>(resources);
            mockDataReader.Setup(m => m.Read()).Returns(() => queue.Count > 0);
            mockDataReader.Setup(m => m.GetOrdinal("Id")).Returns(0);
            mockDataReader.Setup(m => m.GetOrdinal("UserId")).Returns(1);
            mockDataReader.Setup(m => m.GetOrdinal("ResourceId")).Returns(2);
            mockDataReader.Setup(m => m.GetOrdinal("Quantity")).Returns(3);
            mockDataReader.Setup(m => m.GetGuid(0)).Returns(() => queue.Peek().Id);
            mockDataReader.Setup(m => m.GetGuid(1)).Returns(() => queue.Peek().OwnerId);
            mockDataReader.Setup(m => m.GetGuid(2)).Returns(() => queue.Peek().ResourceId);
            mockDataReader.Setup(m => m.GetInt32(3)).Returns(() => queue.Dequeue().Quantity);
        }

        [TestMethod]
        public async Task GetUserResourceByResourceIdAsync_ValidIds_ReturnsResource()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            Guid resourceId = Guid.NewGuid();
            InventoryResource expectedResource = new InventoryResource(Guid.NewGuid(), userId, resourceId, 15);

            SetupMockReaderForSingleResource(expectedResource);
            var parameters = new Dictionary<string, object>
    {
        { "@UserId", userId },
        { "@ResourceId", resourceId }
    };
            mockDatabaseProvider.Setup(m => m.ExecuteReaderAsync("SELECT * FROM InventoryResources WHERE UserId = @UserId AND ResourceId = @ResourceId", parameters))
                                .ReturnsAsync(mockDataReader.Object);

            // Act
            InventoryResource result = await repository.GetUserResourceByResourceIdAsync(userId, resourceId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResource.Quantity, result.Quantity);
        }

        private void SetupMockReaderForSingleResource(InventoryResource resource)
        {
            mockDataReader.Setup(m => m.Read()).Returns(true);
            mockDataReader.Setup(m => m.GetOrdinal("Id")).Returns(0);
            mockDataReader.Setup(m => m.GetOrdinal("UserId")).Returns(1);
            mockDataReader.Setup(m => m.GetOrdinal("ResourceId")).Returns(2);
            mockDataReader.Setup(m => m.GetOrdinal("Quantity")).Returns(3);
            mockDataReader.Setup(m => m.GetGuid(0)).Returns(resource.Id);
            mockDataReader.Setup(m => m.GetGuid(1)).Returns(resource.OwnerId);
            mockDataReader.Setup(m => m.GetGuid(2)).Returns(resource.ResourceId);
            mockDataReader.Setup(m => m.GetInt32(3)).Returns(resource.Quantity);
        }

        [TestMethod]
        public async Task AddUserResourceAsync_ValidResource_CallsExecuteReaderAsyncWithCorrectParameters()
        {
            // Arrange
            InventoryResource userResource = new InventoryResource(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), 25);
            var parameters = new Dictionary<string, object>
    {
        { "@Id", userResource.Id },
        { "@UserId", userResource.OwnerId },
        { "@ResourceId", userResource.ResourceId },
        { "@Quantity", userResource.Quantity }
    };

            // Act
            await repository.AddUserResourceAsync(userResource);

            // Assert
            mockDatabaseProvider.Verify(m => m.ExecuteReaderAsync(
                "INSERT INTO InventoryResources (Id, UserId, ResourceId, Quantity) VALUES (@Id, @UserId, @ResourceId, @Quantity)",
                parameters), Times.Once);
        }

        [TestMethod]
        public async Task UpdateUserResourceAsync_ValidResource_UpdatesResourceQuantity()
        {
            // Arrange
            InventoryResource userResource = new InventoryResource(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), 50);
            var parameters = new Dictionary<string, object>
    {
        { "@Id", userResource.Id },
        { "@Quantity", userResource.Quantity }
    };

            // Act
            await repository.UpdateUserResourceAsync(userResource);

            // Assert
            mockDatabaseProvider.Verify(m => m.ExecuteReaderAsync(
                "UPDATE InventoryResources SET Quantity = @Quantity WHERE Id = @Id",
                parameters), Times.Once);
        }

        [TestMethod]
        public async Task DeleteUserResourceAsync_ValidId_DeletesResource()
        {
            // Arrange
            Guid userResourceId = Guid.NewGuid();
            var parameters = new Dictionary<string, object> { { "@Id", userResourceId } };

            // Act
            await repository.DeleteUserResourceAsync(userResourceId);

            // Assert
            mockDatabaseProvider.Verify(m => m.ExecuteReaderAsync(
                "DELETE FROM InventoryResources WHERE Id = @Id",
                parameters), Times.Once);
        }

    }
}
