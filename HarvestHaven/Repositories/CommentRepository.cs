using Microsoft.Data.SqlClient;
using HarvestHaven.Utils;
using HarvestHaven.Entities;

namespace HarvestHaven.Repositories
{
    public static class CommentRepository
    {
        private static readonly string _connectionString = DatabaseHelper.GetDatabaseFilePath();

        public static async Task CreateCommentAsync(Comment comment)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
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

        public static async Task<List<Comment>> GetUserCommentsAsync(Guid userId)
        {
            List<Comment> userComments = new List<Comment>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
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
                            userComments.Add(new Comment
                            (
                                id: (Guid)reader["Id"],
                                userId: (Guid)reader["UserId"],
                                message: (string)reader["Message"],
                                createdTime: (DateTime)reader["CreatedTime"]
                            ));
                        }
                    }
                }
            }
            return userComments;
        }

        public static async Task UpdateCommentAsync(Comment comment)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
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

        public static async Task DeleteCommentAsync(Guid commentId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
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
