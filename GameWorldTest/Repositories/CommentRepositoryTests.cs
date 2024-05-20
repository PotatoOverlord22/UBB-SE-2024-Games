using GameWorld.Entities;
using GameWorld.Resources.Utils;
using Moq;
using System.Data;

namespace GameWorld.Repositories.Tests
{
    [TestClass()]
    public class CommentRepositoryTests
    {
        private Mock<IDatabaseProvider> mockDatabaseProvider;
        private CommentRepository repository;
        private Mock<IDataReader> mockDataReader;

        [TestInitialize]
        public void Initialize()
        {
            mockDatabaseProvider = new Mock<IDatabaseProvider>();
            repository = new CommentRepository(mockDatabaseProvider.Object);
            mockDataReader = new Mock<IDataReader>();
        }

        [TestMethod]
        public async Task CreateCommentAsync_ValidComment_CallsExecuteReaderAsyncWithCorrectParameters()
        {
            // Arrange
            Comment comment = new Comment(Guid.NewGuid(), Guid.NewGuid(), "Test Message", DateTime.Now);

            var parameters = new Dictionary<string, object>
            {
                { "@Id", comment.Id },
                { "@UserId", comment.PosterUserId },
                { "@Message", comment.CommentMessage },
                { "@CreatedTime", comment.CreationTime }
            };

            // Act
            await repository.CreateCommentAsync(comment);

            // Assert
            mockDatabaseProvider.Verify(m => m.ExecuteReaderAsync(
                "INSERT INTO Comments (Id, UserId, Message, CreatedTime) VALUES (@Id, @UserId, @Message, @CreatedTime)",
                parameters), Times.Once);
        }

        [TestMethod]
        public async Task GetUserCommentsAsync_UserId_ReturnsUserComments()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            List<Comment> expectedComments = new List<Comment>
            {
                new Comment(Guid.NewGuid(), userId, "Message 1", DateTime.Now),
                new Comment(Guid.NewGuid(), userId, "Message 2", DateTime.Now)
            };
            SetupMockReaderForComments(expectedComments);

            var parameters = new Dictionary<string, object> { { "@UserId", userId } };
            mockDatabaseProvider.Setup(m => m.ExecuteReaderAsync("SELECT * FROM Comments WHERE UserId = @UserId", parameters))
                                .ReturnsAsync(mockDataReader.Object);

            // Act
            List<Comment> result = await repository.GetUserCommentsAsync(userId);

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(expectedComments[0].CommentMessage, result[0].CommentMessage);
            Assert.AreEqual(expectedComments[1].CommentMessage, result[1].CommentMessage);
        }

        private void SetupMockReaderForComments(List<Comment> comments)
        {
            var queue = new Queue<Comment>(comments);
            mockDataReader.Setup(m => m.Read()).Returns(() => queue.Count > 0);
            mockDataReader.Setup(m => m.GetOrdinal("Id")).Returns(0);
            mockDataReader.Setup(m => m.GetOrdinal("UserId")).Returns(1);
            mockDataReader.Setup(m => m.GetOrdinal("Message")).Returns(2);
            mockDataReader.Setup(m => m.GetOrdinal("CreatedTime")).Returns(3);
            mockDataReader.Setup(m => m.GetGuid(0)).Returns(() => queue.Peek().Id);
            mockDataReader.Setup(m => m.GetGuid(1)).Returns(() => queue.Peek().PosterUserId);
            mockDataReader.Setup(m => m.GetString(2)).Returns(() => queue.Peek().CommentMessage);
            mockDataReader.Setup(m => m.GetDateTime(3)).Returns(() => queue.Dequeue().CreationTime);
        }

        [TestMethod]
        public async Task UpdateCommentAsync_ValidComment_CallsExecuteReaderAsyncWithCorrectParameters()
        {
            // Arrange
            Comment comment = new Comment(Guid.NewGuid(), Guid.NewGuid(), "Updated Message", DateTime.Now);

            var parameters = new Dictionary<string, object>
            {
                { "@Id", comment.Id },
                { "@Message", comment.CommentMessage }
            };

            // Act
            await repository.UpdateCommentAsync(comment);

            // Assert
            mockDatabaseProvider.Verify(m => m.ExecuteReaderAsync(
                "UPDATE Comments SET Message = @Message WHERE Id = @Id",
                parameters), Times.Once);
        }

        [TestMethod]
        public async Task DeleteCommentAsync_ValidId_CallsExecuteReaderAsyncWithCorrectParameters()
        {
            // Arrange
            Guid commentId = Guid.NewGuid();

            var parameters = new Dictionary<string, object> { { "@Id", commentId } };

            // Act
            await repository.DeleteCommentAsync(commentId);

            // Assert
            mockDatabaseProvider.Verify(m => m.ExecuteReaderAsync(
                "DELETE FROM Comments WHERE Id = @Id",
                parameters), Times.Once);
        }
    }
}
