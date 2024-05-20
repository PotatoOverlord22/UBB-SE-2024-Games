using System.Data;
using GameWorld.Entities;
using GameWorld.Utils;

namespace GameWorld.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IDatabaseProvider databaseProvider;

        public CommentRepository(IDatabaseProvider databaseProvider)
        {
            this.databaseProvider = databaseProvider;
        }

        public async Task CreateCommentAsync(Comment comment)
        {
            var queryParameters = new Dictionary<string, object>
            {
                { "@Id", comment.Id },
                { "@UserId", comment.PosterUserId },
                { "@Message", comment.CommentMessage },
                { "@CreatedTime", comment.CreationTime }
            };

            await databaseProvider.ExecuteReaderAsync(
                "INSERT INTO Comments (Id, UserId, Message, CreatedTime) VALUES (@Id, @UserId, @Message, @CreatedTime)",
                queryParameters);
        }

        public async Task<List<Comment>> GetUserCommentsAsync(Guid userId)
        {
            List<Comment> userComments = new List<Comment>();
            var queryParameters = new Dictionary<string, object> { { "@UserId", userId } };

            using (IDataReader databaseReader = await databaseProvider.ExecuteReaderAsync("SELECT * FROM Comments WHERE UserId = @UserId", queryParameters))
            {
                int idOrdinal = databaseReader.GetOrdinal("Id");
                int userIdOrdinal = databaseReader.GetOrdinal("UserId");
                int messageOrdinal = databaseReader.GetOrdinal("Message");
                int createdTimeOrdinal = databaseReader.GetOrdinal("CreatedTime");

                while (databaseReader.Read())
                {
                    userComments.Add(new Comment(
                        id: databaseReader.GetGuid(idOrdinal),
                        posterUserId: databaseReader.GetGuid(userIdOrdinal),
                        commentMessage: databaseReader.GetString(messageOrdinal),
                        commentCreationTime: databaseReader.GetDateTime(createdTimeOrdinal)));
                }
            }
            return userComments;
        }

        public async Task UpdateCommentAsync(Comment comment)
        {
            var queryParameters = new Dictionary<string, object>
            {
                { "@Id", comment.Id },
                { "@Message", comment.CommentMessage }
            };

            await databaseProvider.ExecuteReaderAsync(
                "UPDATE Comments SET Message = @Message WHERE Id = @Id",
                queryParameters);
        }

        public async Task DeleteCommentAsync(Guid commentId)
        {
            var queryParameters = new Dictionary<string, object> { { "@Id", commentId } };

            await databaseProvider.ExecuteReaderAsync("DELETE FROM Comments WHERE Id = @Id", queryParameters);
        }
    }
}
