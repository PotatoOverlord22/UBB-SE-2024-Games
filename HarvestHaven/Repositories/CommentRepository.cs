using Microsoft.Data.SqlClient;
using HarvestHaven.Utils;
using HarvestHaven.Entities;

namespace HarvestHaven.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly string connectionString = DatabaseHelper.GetDatabaseFilePath();

        public async Task CreateCommentAsync(Comment comment)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "INSERT INTO Comments (Id, UserId, Message, CreatedTime) VALUES (@Id, @UserId, @Message, @CreatedTime)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", comment.Id);
                    command.Parameters.AddWithValue("@UserId", comment.UserId);
                    command.Parameters.AddWithValue("@Message", comment.Message);
                    command.Parameters.AddWithValue("@CreatedTime", comment.CreatedTime);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<List<Comment>> GetUserCommentsAsync(Guid userId)
        {
            List<Comment> userComments = new List<Comment>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT * FROM Comments WHERE UserId = @UserId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            userComments.Add(new Comment(
                                id: (Guid)reader["Id"],
                                userId: (Guid)reader["UserId"],
                                message: (string)reader["Message"],
                                createdTime: (DateTime)reader["CreatedTime"]));
                        }
                    }
                }
            }
            return userComments;
        }

        public async Task UpdateCommentAsync(Comment comment)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "UPDATE Comments SET Message = @Message WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", comment.Id);
                    command.Parameters.AddWithValue("@Message", comment.Message);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteCommentAsync(Guid commentId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "DELETE FROM Comments WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", commentId);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}

