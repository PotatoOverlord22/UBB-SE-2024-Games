using GameWorld.Entities;
using GameWorld.Utils;
using Moq;
using System.Data;

namespace GameWorld.Repositories.Tests
{
    [TestClass]
    public class FarmCellRepositoryTests
    {
        private Mock<IDatabaseProvider> mockDatabaseProvider;
        private FarmCellRepository repository;
        private Mock<IDataReader> mockDataReader;

        [TestInitialize]
        public void Initialize()
        {
            mockDatabaseProvider = new Mock<IDatabaseProvider>();
            repository = new FarmCellRepository(mockDatabaseProvider.Object);
            mockDataReader = new Mock<IDataReader>();
        }


        [TestMethod]
        public async Task GetUserFarmCellByPositionAsync_ValidUserIdAndPosition_ReturnsFarmCell()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            int row = 1;
            int column = 2;
            FarmCell expectedFarmCell = new FarmCell(Guid.NewGuid(), userId, row, column, Guid.NewGuid(), DateTime.Now, DateTime.Now);
            var parameters = new Dictionary<string, object>
            {
                { "@UserId", userId },
                { "@Row", row },
                { "@Column", column }
            };

            SetupMockReaderForSingleFarmCell(expectedFarmCell);
            mockDatabaseProvider.Setup(m => m.ExecuteReaderAsync("SELECT * FROM FarmCells WHERE UserId = @UserId AND Row = @Row AND [Column] = @Column", parameters))
                                .ReturnsAsync(mockDataReader.Object);

            // Act
            FarmCell result = await repository.GetUserFarmCellByPositionAsync(userId, row, column);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedFarmCell.Id, result.Id);
        }

        private void SetupMockReaderForSingleFarmCell(FarmCell farmCell)
        {
            mockDataReader.Setup(m => m.Read()).Returns(true);
            mockDataReader.Setup(m => m.GetOrdinal("Id")).Returns(0);
            mockDataReader.Setup(m => m.GetOrdinal("UserId")).Returns(1);
            mockDataReader.Setup(m => m.GetOrdinal("Row")).Returns(2);
            mockDataReader.Setup(m => m.GetOrdinal("Column")).Returns(3);
            mockDataReader.Setup(m => m.GetOrdinal("ItemId")).Returns(4);
            mockDataReader.Setup(m => m.GetOrdinal("LastTimeEnhanced")).Returns(5);
            mockDataReader.Setup(m => m.GetOrdinal("LastTimeInteracted")).Returns(6);
            mockDataReader.Setup(m => m.GetGuid(0)).Returns(farmCell.Id);
            mockDataReader.Setup(m => m.GetGuid(1)).Returns(farmCell.UserId);
            mockDataReader.Setup(m => m.GetInt32(2)).Returns(farmCell.Row);
            mockDataReader.Setup(m => m.GetInt32(3)).Returns(farmCell.Column);
            mockDataReader.Setup(m => m.GetGuid(4)).Returns(farmCell.ItemId);
            mockDataReader.Setup(m => m.GetDateTime(5)).Returns(farmCell.LastTimeEnhanced ?? DateTime.Now);
            mockDataReader.Setup(m => m.GetDateTime(6)).Returns(farmCell.LastTimeInteracted ?? DateTime.Now);
            mockDataReader.Setup(m => m.IsDBNull(It.IsAny<int>())).Returns((int i) => i == 5 && farmCell.LastTimeEnhanced == null || i == 6 && farmCell.LastTimeInteracted == null);
        }

        [TestMethod]
        public async Task AddFarmCellAsync_ValidFarmCell_CallsExecuteReaderAsyncWithCorrectParameters()
        {
            // Arrange
            FarmCell farmCell = new FarmCell(Guid.NewGuid(), Guid.NewGuid(), 3, 4, Guid.NewGuid(), DateTime.Now, DateTime.Now);
            var parameters = new Dictionary<string, object>
            {
                { "@Id", farmCell.Id },
                { "@UserId", farmCell.UserId },
                { "@Row", farmCell.Row },
                { "@Column", farmCell.Column },
                { "@ItemId", farmCell.ItemId },
                { "@LastTimeEnhanced", farmCell.LastTimeEnhanced ?? (object)DBNull.Value },
                { "@LastTimeInteracted", farmCell.LastTimeInteracted ?? (object)DBNull.Value }
            };

            // Act
            await repository.AddFarmCellAsync(farmCell);

            // Assert
            mockDatabaseProvider.Verify(m => m.ExecuteReaderAsync(
                "INSERT INTO FarmCells (Id, UserId, Row, [Column], ItemId, LastTimeEnhanced, LastTimeInteracted) VALUES (@Id, @UserId, @Row, @Column, @ItemId, @LastTimeEnhanced, @LastTimeInteracted)",
                parameters), Times.Once);
        }

        [TestMethod]
        public async Task UpdateFarmCellAsync_ValidFarmCell_UpdatesFarmCellDetails()
        {
            // Arrange
            FarmCell farmCell = new FarmCell(Guid.NewGuid(), Guid.NewGuid(), 5, 6, Guid.NewGuid(), DateTime.Now, DateTime.Now);
            var parameters = new Dictionary<string, object>
            {
                { "@Id", farmCell.Id },
                { "@Row", farmCell.Row },
                { "@Column", farmCell.Column },
                { "@ItemId", farmCell.ItemId },
                { "@LastTimeEnhanced", farmCell.LastTimeEnhanced ?? (object)DBNull.Value },
                { "@LastTimeInteracted", farmCell.LastTimeInteracted ?? (object)DBNull.Value }
            };

            // Act
            await repository.UpdateFarmCellAsync(farmCell);

            // Assert
            mockDatabaseProvider.Verify(m => m.ExecuteReaderAsync(
                "UPDATE FarmCells SET Row = @Row, [Column] = @Column, ItemId = @ItemId, LastTimeEnhanced = @LastTimeEnhanced, LastTimeInteracted = @LastTimeInteracted WHERE Id = @Id",
                parameters), Times.Once);
        }

        [TestMethod]
        public async Task DeleteFarmCellAsync_ValidId_DeletesFarmCell()
        {
            // Arrange
            Guid farmCellId = Guid.NewGuid();
            var parameters = new Dictionary<string, object> { { "@Id", farmCellId } };

            // Act
            await repository.DeleteFarmCellAsync(farmCellId);

            // Assert
            mockDatabaseProvider.Verify(m => m.ExecuteReaderAsync("DELETE FROM FarmCells WHERE Id = @Id", parameters), Times.Once);
        }

    }
}
