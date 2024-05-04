using Microsoft.Data.SqlClient;
using HarvestHaven.Utils;
using HarvestHaven.Entities;

namespace HarvestHaven.Repositories
{
    public static class UserAchievementRepository
    {
        private static readonly string _connectionString = DatabaseHelper.GetDatabaseFilePath();

        public static async Task<List<UserAchievement>> GetAllUserAchievementsAsync(Guid userId)
        {
            List<UserAchievement> userAchievements = new List<UserAchievement>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("SELECT * FROM UserAchievements WHERE UserId = @UserId", connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            userAchievements.Add(new UserAchievement(
                                id: (Guid)reader["Id"],
                                userId: (Guid)reader["UserId"],
                                achievementId: (Guid)reader["AchievementId"],
                                createdTime: (DateTime)reader["CreatedTime"]
                            ));
                        }
                    }
                }
            }
            return userAchievements;
        }

        public static async Task AddUserAchievementAsync(UserAchievement userAchievement)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "INSERT INTO UserAchievements (Id, UserId, AchievementId, CreatedTime) VALUES (@Id, @UserId, @AchievementId, @CreatedTime)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", userAchievement.Id);
                    command.Parameters.AddWithValue("@UserId", userAchievement.UserId);
                    command.Parameters.AddWithValue("@AchievementId", userAchievement.AchievementId);
                    command.Parameters.AddWithValue("@CreatedTime", userAchievement.CreatedTime);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public static async Task UpdateUserAchievementAsync(UserAchievement userAchievement)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "UPDATE UserAchievements SET UserId = @UserId, AchievementId = @AchievementId, CreatedTime = @CreatedTime WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", userAchievement.Id);
                    command.Parameters.AddWithValue("@UserId", userAchievement.UserId);
                    command.Parameters.AddWithValue("@AchievementId", userAchievement.AchievementId);
                    command.Parameters.AddWithValue("@CreatedTime", userAchievement.CreatedTime);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public static async Task DeleteUserAchievementAsync(Guid userAchievementId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "DELETE FROM UserAchievements WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", userAchievementId);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
