using System.Data;
using Microsoft.Data.SqlClient;
using HarvestHaven.Utils;
using HarvestHaven.Entities;

namespace HarvestHaven.Repositories
{
    public class AchievementRepository : IAchievementRepository
    {
        private readonly string connectionString = DatabaseHelper.GetDatabaseFilePath();
        private readonly IDatabaseProvider databaseProvider;

        public AchievementRepository(IDatabaseProvider databaseProvider)
        {
            this.databaseProvider = databaseProvider;
        }

        public async Task<List<Achievement>> GetAllAchievementsAsync()
        {
            List<Achievement> achievements = new List<Achievement>();

            using (IDataReader reader = await databaseProvider.ExecuteReaderAsync("SELECT * FROM Achievements", null))
            {
                int idOrdinal = reader.GetOrdinal("Id");
                int descriptionOrdinal = reader.GetOrdinal("Description");
                int rewardCoinsOrdinal = reader.GetOrdinal("RewardCoins");

                while (reader.Read())
                {
                    Guid id = reader.GetGuid(idOrdinal);
                    string description = reader.GetString(descriptionOrdinal);
                    int rewardCoins = reader.GetInt32(rewardCoinsOrdinal);

                    achievements.Add(new Achievement(id, description, rewardCoins));
                }
            }

            return achievements;
        }

        public async Task<Achievement> GetAchievementByIdAsync(Guid achievementId)
        {
            Achievement achievement = null;

            var parameters = new Dictionary<string, object>
            {
                 { "@Id", achievementId }
            };

            using (IDataReader reader = await databaseProvider.ExecuteReaderAsync("SELECT * FROM Achievements WHERE Id = @Id", parameters))
            {
                int idOrdinal = reader.GetOrdinal("Id");
                int descriptionOrdinal = reader.GetOrdinal("Description");
                int rewardCoinsOrdinal = reader.GetOrdinal("RewardCoins");
                if (reader.Read())
                {
                    Guid id = reader.GetGuid(idOrdinal);
                    string description = reader.GetString(descriptionOrdinal);
                    int rewardCoins = reader.GetInt32(rewardCoinsOrdinal);

                   achievement = new Achievement(id, description, rewardCoins);
                }
            }

            return achievement;
        }

        public async Task AddAchievementAsync(Achievement achievement)
        {
            var parameters = new Dictionary<string, object>
            {
                { "@Id", achievement.Id },
                { "@Description", achievement.Description },
                { "@RewardCoins", achievement.RewardCoins }
             };

            using (IDataReader reader = await databaseProvider.ExecuteReaderAsync("INSERT INTO Achievements (Id, Description, RewardCoins) VALUES (@Id, @Description, @RewardCoins)", parameters))
            {
            }
        }

        public async Task UpdateAchievementAsync(Achievement achievement)
        {
            var parameters = new Dictionary<string, object>
            {
                { "@Id", achievement.Id },
                { "@Description", achievement.Description },
                { "@RewardCoins", achievement.RewardCoins }
            };

            using (IDataReader reader = await databaseProvider.ExecuteReaderAsync("UPDATE Achievements SET Description = @Description, RewardCoins = @RewardCoins WHERE Id = @Id", parameters))
            {
            }
        }
        public async Task DeleteAchievementAsync(Guid achievementId)
        {
            var parameters = new Dictionary<string, object>
            {
                { "@Id", achievementId }
            };

            using (IDataReader reader = await databaseProvider.ExecuteReaderAsync("DELETE FROM Achievements WHERE Id = @Id", parameters))
            {
            }
        }
    }
}
