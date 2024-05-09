using System.Data;
using HarvestHaven.Entities;
using HarvestHaven.Utils;
namespace HarvestHaven.Repositories
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
            var parameters = new Dictionary<string, object>
            {
                { "@Id", comment.Id },
                { "@UserId", comment.UserId },
                { "@Message", comment.Message },
                { "@CreatedTime", comment.CreatedTime }
            };

            await databaseProvider.ExecuteReaderAsync(
                "INSERT INTO Comments (Id, UserId, Message, CreatedTime) VALUES (@Id, @UserId, @Message, @CreatedTime)",
                parameters);
        }

        public async Task<List<Comment>> GetUserCommentsAsync(Guid userId)
        {
            List<Comment> userComments = new List<Comment>();
            var parameters = new Dictionary<string, object> { { "@UserId", userId } };

            using (IDataReader reader = await databaseProvider.ExecuteReaderAsync("SELECT * FROM Comments WHERE UserId = @UserId", parameters))
            {
                int idOrdinal = reader.GetOrdinal("Id");
                int userIdOrdinal = reader.GetOrdinal("UserId");
                int messageOrdinal = reader.GetOrdinal("Message");
                int createdTimeOrdinal = reader.GetOrdinal("CreatedTime");

                while (reader.Read())
                {
                    userComments.Add(new Comment(
                        id: reader.GetGuid(idOrdinal),
                        userId: reader.GetGuid(userIdOrdinal),
                        message: reader.GetString(messageOrdinal),
                        createdTime: reader.GetDateTime(createdTimeOrdinal)));
                }
            }
            return userComments;
        }

        public async Task UpdateCommentAsync(Comment comment)
        {
            var parameters = new Dictionary<string, object>
            {
                { "@Id", comment.Id },
                { "@Message", comment.Message }
            };

            await databaseProvider.ExecuteReaderAsync(
                "UPDATE Comments SET Message = @Message WHERE Id = @Id",
                parameters);
        }

        public async Task DeleteCommentAsync(Guid commentId)
        {
            var parameters = new Dictionary<string, object> { { "@Id", commentId } };

            await databaseProvider.ExecuteReaderAsync("DELETE FROM Comments WHERE Id = @Id", parameters);
        }
    }
}
