using Microsoft.Data.SqlClient;
using HarvestHaven.Utils;
using HarvestHaven.Entities;

namespace HarvestHaven.Repositories
{
    public static class FarmCellRepository
    {
        private static readonly string _connectionString = DatabaseHelper.GetDatabaseFilePath();

        public static async Task<List<FarmCell>> GetUserFarmCellsAsync(Guid userId)
        {
            List<FarmCell> farmCells = new List<FarmCell>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT * FROM FarmCells WHERE UserId = @UserId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            farmCells.Add(new FarmCell
                            (
                                id: (Guid)reader["Id"],
                                userId: (Guid)reader["UserId"],
                                row: (int)reader["Row"],
                                column: (int)reader["Column"],
                                itemId: (Guid)reader["ItemId"],
                                lastTimeEnhanced: reader["LastTimeEnhanced"] != DBNull.Value ? (DateTime?)reader["LastTimeEnhanced"] : null,
                                lastTimeInteracted: reader["LastTimeInteracted"] != DBNull.Value ? (DateTime?)reader["LastTimeInteracted"] : null
                            ));
                        }
                    }
                }
            }
            return farmCells;
        }

        public static async Task<FarmCell> GetUserFarmCellByPositionAsync(Guid userId, int row, int column)
        {
            FarmCell farmCell = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT * FROM FarmCells WHERE UserId = @UserId AND Row = @Row AND [Column] = @Column";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@Row", row);
                    command.Parameters.AddWithValue("@Column", column);
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            farmCell = new FarmCell
                            (
                                id: (Guid)reader["Id"],
                                userId: (Guid)reader["UserId"],
                                row: (int)reader["Row"],
                                column: (int)reader["Column"],
                                itemId: (Guid)reader["ItemId"],
                                lastTimeEnhanced: reader["LastTimeEnhanced"] != DBNull.Value ? (DateTime?)reader["LastTimeEnhanced"] : null,
                                lastTimeInteracted: reader["LastTimeInteracted"] != DBNull.Value ? (DateTime?)reader["LastTimeInteracted"] : null
                            );
                        }
                    }
                }
            }
            return farmCell;
        }

        public static async Task AddFarmCellAsync(FarmCell farmCell)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "INSERT INTO FarmCells (Id, UserId, Row, [Column], ItemId, LastTimeEnhanced, LastTimeInteracted) VALUES (@Id, @UserId, @Row, @Column, @ItemId, @LastTimeEnhanced, @LastTimeInteracted)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", farmCell.Id);
                    command.Parameters.AddWithValue("@UserId", farmCell.UserId);
                    command.Parameters.AddWithValue("@Row", farmCell.Row);
                    command.Parameters.AddWithValue("@Column", farmCell.Column);
                    command.Parameters.AddWithValue("@ItemId", farmCell.ItemId);
                    command.Parameters.AddWithValue("@LastTimeEnhanced", farmCell.LastTimeEnhanced ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@LastTimeInteracted", farmCell.LastTimeInteracted ?? (object)DBNull.Value);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public static async Task UpdateFarmCellAsync(FarmCell farmCell)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "UPDATE FarmCells SET Row = @Row, [Column] = @Column, ItemId = @ItemId, LastTimeEnhanced = @LastTimeEnhanced, LastTimeInteracted = @LastTimeInteracted WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", farmCell.Id);
                    command.Parameters.AddWithValue("@Row", farmCell.Row);
                    command.Parameters.AddWithValue("@Column", farmCell.Column);
                    command.Parameters.AddWithValue("@ItemId", farmCell.ItemId);
                    command.Parameters.AddWithValue("@LastTimeEnhanced", farmCell.LastTimeEnhanced ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@LastTimeInteracted", farmCell.LastTimeInteracted ?? (object)DBNull.Value);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public static async Task DeleteFarmCellAsync(Guid farmCellId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "DELETE FROM FarmCells WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", farmCellId);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
