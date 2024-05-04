using Microsoft.Data.SqlClient;
using HarvestHaven.Utils;
using HarvestHaven.Entities;
using Microsoft.VisualBasic.ApplicationServices;
using System.Diagnostics;
using System.Windows.Forms;

namespace HarvestHaven.Repositories
{
    public static class TradeRepository
    {
        private static readonly string _connectionString = DatabaseHelper.GetDatabaseFilePath();

        public static async Task<List<Trade>> GetAllTradesAsync()
        {
            List<Trade> trades = new List<Trade>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Trades", connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            trades.Add(new Trade
                            (
                                id: (Guid)reader["Id"],
                                userId: (Guid)reader["UserId"],
                                givenResourceId: (Guid)reader["GivenResourceId"],
                                givenResourceQuantity: (int)reader["GivenResourceQuantity"],
                                requestedResourceId: (Guid)reader["RequestedResourceId"],
                                requestedResourceQuantity: (int)reader["RequestedResourceQuantity"],
                                createdTime: (DateTime)reader["CreatedTime"],
                                isCompleted: (bool)reader["IsCompleted"]
                            ));
                        }
                    }
                }
            }
            return trades;
        }

        public static async Task<List<Trade>> GetAllTradesExceptCreatedByUser(Guid userId)
        {
            List<Trade> trades = new List<Trade>();
            string query = "SELECT * FROM Trades WHERE UserId <> @UserId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Trade trade = new Trade
                            (
                                id: (Guid)reader["Id"],
                                userId: (Guid)reader["UserId"],
                                givenResourceId: (Guid)reader["GivenResourceId"],
                                givenResourceQuantity: (int)reader["GivenResourceQuantity"],
                                requestedResourceId: (Guid)reader["RequestedResourceId"],
                                requestedResourceQuantity: (int)reader["RequestedResourceQuantity"],
                                createdTime: (DateTime)reader["CreatedTime"],
                                isCompleted: (bool)reader["IsCompleted"]
                            );
                            trades.Add(trade);
                        }
                    }
                }
            }
            return trades;
        }

        public static async Task<Trade> GetTradeByIdAsync(Guid tradeId)
        {
            Trade? trade = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Trades Where Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", tradeId);
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            trade = new Trade
                            (
                                id: (Guid)reader["Id"],
                                userId: (Guid)reader["UserId"],
                                givenResourceId: (Guid)reader["GivenResourceId"],
                                givenResourceQuantity: (int)reader["GivenResourceQuantity"],
                                requestedResourceId: (Guid)reader["RequestedResourceId"],
                                requestedResourceQuantity: (int)reader["RequestedResourceQuantity"],
                                createdTime: (DateTime)reader["CreatedTime"],
                                isCompleted: (bool)reader["IsCompleted"]
                            );
                        }
                    }
                }
            }
            return trade;
        }

        public static async Task<Trade> GetUserTradeAsync(Guid userId)
        {
            Trade userTrade = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT * FROM Trades WHERE UserId = @UserId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            userTrade = new Trade
                            (
                                id: (Guid)reader["Id"],
                                userId: (Guid)reader["UserId"],
                                givenResourceId: (Guid)reader["GivenResourceId"],
                                givenResourceQuantity: (int)reader["GivenResourceQuantity"],
                                requestedResourceId: (Guid)reader["RequestedResourceId"],
                                requestedResourceQuantity: (int)reader["RequestedResourceQuantity"],
                                createdTime: (DateTime)reader["CreatedTime"],
                                isCompleted: (bool)reader["IsCompleted"]
                            );
                        }
                    }
                }
            }
            return userTrade;
        }

        public static async Task CreateTradeAsync(Trade trade)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "INSERT INTO Trades (Id, UserId, GivenResourceId, GivenResourceQuantity, RequestedResourceId, RequestedResourceQuantity, CreatedTime, IsCompleted) VALUES (@Id, @UserId, @GivenResourceId, @GivenResourceQuantity, @RequestedResourceId, @RequestedResourceQuantity, @CreatedTime, @IsCompleted)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", trade.Id);
                    command.Parameters.AddWithValue("@UserId", trade.UserId);
                    command.Parameters.AddWithValue("@GivenResourceId", trade.GivenResourceId);
                    command.Parameters.AddWithValue("@GivenResourceQuantity", trade.GivenResourceQuantity);
                    command.Parameters.AddWithValue("@RequestedResourceId", trade.RequestedResourceId);
                    command.Parameters.AddWithValue("@RequestedResourceQuantity", trade.RequestedResourceQuantity);
                    command.Parameters.AddWithValue("@CreatedTime", trade.CreatedTime);
                    command.Parameters.AddWithValue("@IsCompleted", trade.IsCompleted);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public static async Task UpdateTradeAsync(Trade trade)
        {
            // Create the SQL connection and release the resources after use.
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("UPDATE Trades SET IsCompleted = @IsCompleted WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", trade.Id);
                    command.Parameters.AddWithValue("@IsCompleted", trade.IsCompleted);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public static async Task DeleteTradeAsync(Guid tradeId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "DELETE FROM Trades WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", tradeId);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
