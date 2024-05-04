using Microsoft.Data.SqlClient;
using HarvestHaven.Utils;
using HarvestHaven.Entities;

namespace HarvestHaven.Repositories
{
    public static class ItemRepository
    {
        private static readonly string _connectionString = DatabaseHelper.GetDatabaseFilePath();

        public static async Task<List<Item>> GetAllItemsAsync()
        {
            List<Item> items = new List<Item>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Items", connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            items.Add(new Item
                            (
                                id: (Guid)reader["Id"],
                                itemType: ((string)reader["ItemType"]).ToEnum<ItemType>(),
                                requiredResourceId: (Guid)reader["RequiredResourceId"],
                                interactResourceId: (Guid)reader["InteractResourceId"],
                                destroyResourceId: reader["DestroyResourceId"] != DBNull.Value ? (Guid?)reader["DestroyResourceId"] : null
                            ));
                        }
                    }
                }
            }
            return items;
        }

        public static async Task<Item> GetItemByIdAsync(Guid itemId)
        {
            Item item = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT * FROM Items WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", itemId);
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            item = new Item
                            (
                                id: (Guid)reader["Id"],
                                itemType: ((string)reader["ItemType"]).ToEnum<ItemType>(),
                                requiredResourceId: (Guid)reader["RequiredResourceId"],
                                interactResourceId: (Guid)reader["InteractResourceId"],
                                destroyResourceId: reader["DestroyResourceId"] != DBNull.Value ? (Guid?)reader["DestroyResourceId"] : null
                            );
                        }
                    }
                }
            }
            return item;
        }

        public static async Task<Item> GetItemByTypeAsync(ItemType itemType)
        {
            Item item = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT * FROM Items WHERE ItemType = @ItemType";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ItemType", itemType.ToString());
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            item = new Item
                            (
                                id: (Guid)reader["Id"],
                                itemType: ((string)reader["ItemType"]).ToEnum<ItemType>(),
                                requiredResourceId: (Guid)reader["RequiredResourceId"],
                                interactResourceId: (Guid)reader["InteractResourceId"],
                                destroyResourceId: reader["DestroyResourceId"] != DBNull.Value ? (Guid?)reader["DestroyResourceId"] : null
                            );
                        }
                    }
                }
            }
            return item;
        }

        public static async Task CreateItemAsync(Item item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "INSERT INTO Items (Id, ItemType, RequiredResourceId, InteractResourceId, DestroyResourceId) VALUES (@Id, @ItemType, @RequiredResourceId, @InteractResourceId, @DestroyResourceId)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@ItemType", item.ItemType.ToString());
                    command.Parameters.AddWithValue("@RequiredResourceId", item.RequiredResourceId);
                    command.Parameters.AddWithValue("@InteractResourceId", item.InteractResourceId);
                    command.Parameters.AddWithValue("@DestroyResourceId", item.DestroyResourceId ?? (object)DBNull.Value);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public static async Task UpdateItemAsync(Item item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "UPDATE Items SET ItemType = @ItemType, RequiredResourceId = @RequiredResourceId, InteractResourceId = @InteractResourceId, DestroyResourceId = @DestroyResourceId WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@ItemType", item.ItemType.ToString());
                    command.Parameters.AddWithValue("@RequiredResourceId", item.RequiredResourceId);
                    command.Parameters.AddWithValue("@InteractResourceId", item.InteractResourceId);
                    command.Parameters.AddWithValue("@DestroyResourceId", item.DestroyResourceId ?? (object)DBNull.Value);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public static async Task DeleteItemAsync(Guid itemId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "DELETE FROM Items WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", itemId);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
