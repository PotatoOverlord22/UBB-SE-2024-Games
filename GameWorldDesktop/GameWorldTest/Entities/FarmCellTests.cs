using GameWorld.Models;

namespace GameWorld.Entities.Tests
{
    [TestClass()]
    public class FarmCellTests
    {
        [TestMethod()]
        public void Constructor_WithValidParameters_InitializesProperties()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            Guid userId = Guid.NewGuid();
            int row = 1;
            int column = 2;
            Guid itemId = Guid.NewGuid();
            DateTime? lastTimeEnhanced = DateTime.UtcNow;
            DateTime? lastTimeInteracted = DateTime.UtcNow;

            // Act
            FarmCell farmCell = new FarmCell(id, userId, row, column, itemId, lastTimeEnhanced, lastTimeInteracted);

            // Assert
            Assert.AreEqual(id, farmCell.Id);
            Assert.AreEqual(userId, farmCell.UserId);
            Assert.AreEqual(row, farmCell.Row);
            Assert.AreEqual(column, farmCell.Column);
            Assert.AreEqual(itemId, farmCell.ItemId);
            Assert.AreEqual(lastTimeEnhanced, farmCell.LastTimeEnhanced);
            Assert.AreEqual(lastTimeInteracted, farmCell.LastTimeInteracted);
        }
    }
}
