using Microsoft.Data.SqlClient;
using HarvestHaven.Utils;
using HarvestHaven.Entities;

namespace HarvestHaven.Repositories
{
    public class UserAchievementRepository : IUserAchievementRepository
    {
        private readonly string connectionString = DatabaseHelper.GetDatabaseFilePath();

        public async Task<List<UserAchievement>> GetAllUserAchievementsAsync(Guid userId)
        {
            List<UserAchievement> userAchievements = new List<UserAchievement>();
            using (SqlConnection connection = new SqlConnection(connectionString))
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
                                createdTime: (DateTime)reader["CreatedTime"]));
                        }
                    }
                }
            }
            return userAchievements;
        }

        public async Task AddUserAchievementAsync(UserAchievement userAchievement)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "INSERT INTO UserAchievements (Id, UserId, AchievementId, CreatedTime) VALUES (@Id, @UserId, @AchievementId, @CreatedTime)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", userAchievement.Id);
                    command.Parameters.AddWithValue("@UserId", userAchievement.UserId);
                    command.Parameters.AddWithValue("@AchievementId", userAchievement.AchievementId);
                    command.Parameters.AddWithValue("@CreatedTime", userAchievement.AchievementRewardedTime);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateUserAchievementAsync(UserAchievement userAchievement)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "UPDATE UserAchievements SET UserId = @UserId, AchievementId = @AchievementId, CreatedTime = @CreatedTime WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", userAchievement.Id);
                    command.Parameters.AddWithValue("@UserId", userAchievement.UserId);
                    command.Parameters.AddWithValue("@AchievementId", userAchievement.AchievementId);
                    command.Parameters.AddWithValue("@CreatedTime", userAchievement.AchievementRewardedTime);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteUserAchievementAsync(Guid userAchievementId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
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
