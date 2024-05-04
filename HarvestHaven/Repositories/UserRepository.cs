using HarvestHaven.Entities;
using HarvestHaven.Utils;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HarvestHaven.Repositories
{
    public static class UserRepository
    {
        private static readonly string _connectionString = DatabaseHelper.GetDatabaseFilePath();

        #region CRUD

        public static async Task AddUserAsync(User user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("INSERT INTO Users (Id, Username, Coins, NrItemsBought, NrTradesPerformed, TradeHallUnlockTime, LastTimeReceivedWater) VALUES (@Id, @Username, @Coins, @NrItemsBought, @NrTradesPerformed, @TradeHallUnlockTime, @LastTimeReceivedWater)", connection))
                {
                    command.Parameters.AddWithValue("@Id", user.Id);
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@Coins", user.Coins);
                    command.Parameters.AddWithValue("@NrItemsBought", user.NrItemsBought);
                    command.Parameters.AddWithValue("@NrTradesPerformed", user.NrTradesPerformed);
                    command.Parameters.AddWithValue("@TradeHallUnlockTime", user.TradeHallUnlockTime ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@LastTimeReceivedWater", user.LastTimeReceivedWater ?? (object)DBNull.Value);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public static async Task<User> GetUserByIdAsync(Guid userId)
        {
            User? user = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT * FROM Users WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", userId);
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            user = new User
                            (
                                id: (Guid)reader["Id"],
                                username: (string)reader["Username"],
                                coins: (int)reader["Coins"],
                                nrItemsBought: (int)reader["NrItemsBought"],
                                nrTradesPerformed: (int)reader["NrTradesPerformed"],
                                tradeHallUnlockTime: reader["TradeHallUnlockTime"] != DBNull.Value ? (DateTime)reader["TradeHallUnlockTime"] : null,
                                lastTimeReceivedWater: reader["LastTimeReceivedWater"] != DBNull.Value ? (DateTime)reader["LastTimeReceivedWater"] : null
                            );
                        }
                    }
                }
            }
            return user;
        }

        public static async Task<List<User>> GetAllUsersAsync()
        {
            List<User> users = new List<User>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Users", connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            users.Add(new User
                            (
                                id: (Guid)reader["Id"],
                                username: (string)reader["Username"],
                                coins: (int)reader["Coins"],
                                nrItemsBought: (int)reader["NrItemsBought"],
                                nrTradesPerformed: (int)reader["NrTradesPerformed"],
                                tradeHallUnlockTime: reader["TradeHallUnlockTime"] != DBNull.Value ? (DateTime)reader["TradeHallUnlockTime"] : null,
                                lastTimeReceivedWater: reader["LastTimeReceivedWater"] != DBNull.Value ? (DateTime)reader["LastTimeReceivedWater"] : null
                            ));
                        }
                    }
                }
            }
            return users;
        }

        public static async Task UpdateUserAsync(User user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("UPDATE Users SET Username = @Username, Coins = @Coins, NrItemsBought = @NrItemsBought, NrTradesPerformed = @NrTradesPerformed, TradeHallUnlockTime = @TradeHallUnlockTime, LastTimeReceivedWater = @LastTimeReceivedWater WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", user.Id);
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@Coins", user.Coins);
                    command.Parameters.AddWithValue("@NrItemsBought", user.NrItemsBought);
                    command.Parameters.AddWithValue("@NrTradesPerformed", user.NrTradesPerformed);
                    command.Parameters.AddWithValue("@TradeHallUnlockTime", user.TradeHallUnlockTime ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@LastTimeReceivedWater", user.LastTimeReceivedWater ?? (object)DBNull.Value);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public static async Task DeleteUserByIdAsync(Guid userId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("DELETE FROM Users WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", userId);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        #endregion

        #region Helper Functions
        public static async Task TestAsync()
        {
            try
            {
                List<User> users = await GetAllUsersAsync();

                if (users != null && users.Count > 0)
                {
                    Console.WriteLine("Initial Users:");
                    foreach (var user in users)
                    {
                        Console.WriteLine($"Username: {user.Username}, Coins: {user.Coins}");
                    }
                }
                else
                {
                    Console.WriteLine("No users found.");
                }

                User newUser = new User
                (
                    id: Guid.NewGuid(),
                    username: "NewUser",
                    coins: 100,
                    nrItemsBought: 0,
                    nrTradesPerformed: 0,
                    tradeHallUnlockTime: DateTime.UtcNow,
                    lastTimeReceivedWater: DateTime.UtcNow
                );
                await AddUserAsync(newUser);
                Console.WriteLine($"New user added: {newUser.Username}");

                users = await GetAllUsersAsync();
                if (users != null && users.Count > 0)
                {
                    Console.WriteLine("\nUpdated Users:");
                    foreach (var user in users)
                    {
                        Console.WriteLine($"Username: {user.Username}, Coins: {user.Coins}");
                    }
                }
                else
                {
                    Console.WriteLine("No users found.");
                }

                newUser.Coins += 50;
                await UpdateUserAsync(newUser);
                Console.WriteLine($"\nUser {newUser.Username} updated. New coins: {newUser.Coins}");

                // Uncomment to delete the newly added user
                await DeleteUserByIdAsync(newUser.Id);
                Console.WriteLine($"\nUser {newUser.Username} deleted.");

                users = await GetAllUsersAsync();
                if (users != null && users.Count > 0)
                {
                    Console.WriteLine("\nUsers after deletion:");
                    foreach (var user in users)
                    {
                        Console.WriteLine($"Username: {user.Username}, Coins: {user.Coins}");
                    }
                }
                else
                {
                    Console.WriteLine("No users found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
            }
        }

        #endregion
    }
}
