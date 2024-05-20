using GameWorld.Entities;
using GameWorld.Utils;
using Moq;
using System.Data;

namespace GameWorld.Repositories.Tests
{
    [TestClass()]
        public class AchievementRepositoryTests
        {

        [TestMethod()]
        public async Task GetAllAchievements_WhenCalled_ReturnsAllAchievements()
        {
            // Arrange
            var mockDataReader = new Mock<IDataReader>();

            List<Achievement> initialAchievements = new List<Achievement>
            {
                new Achievement(Guid.NewGuid(), "Description1", 100),
                new Achievement(Guid.NewGuid(), "Description2", 200)
            };

            var achievementsData = new Queue<(Guid, string, int)>(initialAchievements.Select(a => (a.Id, a.Description, a.NumberOfCoinsRewarded)));

            mockDataReader.SetupSequence(m => m.Read())
                  .Returns(() => achievementsData.Count > 0)
                  .Returns(() => achievementsData.Count > 0)
                  .Returns(false);

            mockDataReader.Setup(m => m.GetOrdinal("Id")).Returns(0);
            mockDataReader.Setup(m => m.GetGuid(0))
                          .Returns(() => achievementsData.Peek().Item1); 
            mockDataReader.Setup(m => m.GetOrdinal("Description")).Returns(1);
            mockDataReader.Setup(m => m.GetString(1))
                          .Returns(() => achievementsData.Peek().Item2); 
            mockDataReader.Setup(m => m.GetOrdinal("RewardCoins")).Returns(2);
            mockDataReader.Setup(m => m.GetInt32(2))
                          .Returns(() => achievementsData.Dequeue().Item3);

            var mockDatabaseProvider = new Mock<IDatabaseProvider>();
            mockDatabaseProvider.Setup(m => m.ExecuteReaderAsync("SELECT * FROM Achievements", null))
                                .ReturnsAsync(mockDataReader.Object);

            var repository = new AchievementRepository(mockDatabaseProvider.Object);

            // Act
            List<Achievement> result = await repository.GetAllAchievementsAsync();

            // Assert
            Assert.AreEqual(2, result.Count);

            Assert.AreEqual(initialAchievements[0].Id, result[0].Id);
            Assert.AreEqual(initialAchievements[0].Description, result[0].Description);
            Assert.AreEqual(initialAchievements[0].NumberOfCoinsRewarded, result[0].NumberOfCoinsRewarded);
        }

        [TestMethod]
        public async Task GetAchievementByIdAsync_WhenCalledWithValidId_ReturnsAchievement()
        {
            // Arrange
            Guid achievementId = Guid.NewGuid();
            var expectedAchievement = new Achievement(achievementId, "Sample Achievement", 100);

            // Mock the database provider
            var mockDataReader = new Mock<IDataReader>();
            mockDataReader.Setup(m => m.Read())
                          .Returns(true);
            mockDataReader.Setup(m => m.GetOrdinal("Id"))
                          .Returns(0);
            mockDataReader.Setup(m => m.GetGuid(0))
                          .Returns(expectedAchievement.Id);
            mockDataReader.Setup(m => m.GetOrdinal("Description"))
                          .Returns(1);
            mockDataReader.Setup(m => m.GetString(1))
                          .Returns(expectedAchievement.Description);
            mockDataReader.Setup(m => m.GetOrdinal("RewardCoins"))
                          .Returns(2);
            mockDataReader.Setup(m => m.GetInt32(2))
                          .Returns(expectedAchievement.NumberOfCoinsRewarded);

            var mockDatabaseProvider = new Mock<IDatabaseProvider>();
            mockDatabaseProvider.Setup(m => m.ExecuteReaderAsync("SELECT * FROM Achievements WHERE Id = @Id", It.IsAny<Dictionary<string, object>>()))
                                .ReturnsAsync(mockDataReader.Object);

            var repository = new AchievementRepository(mockDatabaseProvider.Object);

            // Act
            Achievement result = await repository.GetAchievementByIdAsync(achievementId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedAchievement.Id, result.Id);
            Assert.AreEqual(expectedAchievement.Description, result.Description);
            Assert.AreEqual(expectedAchievement.NumberOfCoinsRewarded, result.NumberOfCoinsRewarded);
        }

        [TestMethod()]
        public async Task AddAchievementAsync_WhenCalledWithValidParameters_NewAchievementAdded()
        {
            // Arrange
            var mockDatabaseProvider = new Mock<IDatabaseProvider>();
            var repository = new AchievementRepository(mockDatabaseProvider.Object);

            Achievement achievementToAdd = new Achievement(Guid.NewGuid(), "Test Description", 50);

            var expectedParameters = new Dictionary<string, object>
            {
                { "@Id", achievementToAdd.Id },
                { "@Description", achievementToAdd.Description },
                { "@RewardCoins", achievementToAdd.NumberOfCoinsRewarded }
            };

            // Act
            await repository.AddAchievementAsync(achievementToAdd);

            // Assert
            mockDatabaseProvider.Verify(m => m.ExecuteReaderAsync(
                "INSERT INTO Achievements (Id, Description, RewardCoins) VALUES (@Id, @Description, @RewardCoins)",
                expectedParameters), Times.Once);
        }

        [TestMethod()]
        public async Task UpdateAchievementAsync_CallsExecuteReaderAsync_WithCorrectParameters()
        {
            // Arrange
            var mockDatabaseProvider = new Mock<IDatabaseProvider>();
            var repository = new AchievementRepository(mockDatabaseProvider.Object);

            Achievement achievementToUpdate = new Achievement(Guid.NewGuid(), "Test Description", 50);

            var expectedParameters = new Dictionary<string, object>
            {
                { "@Id", achievementToUpdate.Id },
                { "@Description", achievementToUpdate.Description },
                { "@RewardCoins", achievementToUpdate.NumberOfCoinsRewarded }
            };

            // Act
            await repository.UpdateAchievementAsync(achievementToUpdate);

            // Assert
            mockDatabaseProvider.Verify(m => m.ExecuteReaderAsync(
                "UPDATE Achievements SET Description = @Description, RewardCoins = @RewardCoins WHERE Id = @Id",
                expectedParameters), Times.Once);
        }

        [TestMethod()]
        public async Task DeleteAchievementAsync_CallsExecuteReaderAsync_WithCorrectParameters()
        {
            // Arrange
            var mockDatabaseProvider = new Mock<IDatabaseProvider>();
            var repository = new AchievementRepository(mockDatabaseProvider.Object);

            Guid achievementIdToDelete = Guid.NewGuid();

            var expectedParameters = new Dictionary<string, object>
            {
                { "@Id", achievementIdToDelete }
            };

            // Act
            await repository.DeleteAchievementAsync(achievementIdToDelete);

            // Assert
            mockDatabaseProvider.Verify(m => m.ExecuteReaderAsync(
                "DELETE FROM Achievements WHERE Id = @Id",
                expectedParameters), Times.Once);
        }
    }
}
