using Microsoft.Data.SqlClient;
using HarvestHaven.Utils;
using HarvestHaven.Entities;

namespace HarvestHaven.Repositories
{
    public class InventoryResourceRepository : IInventoryResourceRepository
    {
        private readonly string connectionString = DatabaseHelper.GetDatabaseFilePath();

        public async Task<List<InventoryResource>> GetUserResourcesAsync(Guid userId)
        {
            List<InventoryResource> userResources = new List<InventoryResource>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT * FROM InventoryResources WHERE UserId = @UserId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            userResources.Add(new InventoryResource(
                                id: (Guid)reader["Id"],
                                userId: (Guid)reader["UserId"],
                                resourceId: (Guid)reader["ResourceId"],
                                quantity: (int)reader["Quantity"]));
                        }
                    }
                }
            }
            return userResources;
        }

        public async Task<InventoryResource> GetUserResourceByResourceIdAsync(Guid userId, Guid resourceId)
        {
            InventoryResource? userResource = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT * FROM InventoryResources WHERE UserId = @UserId AND ResourceId = @ResourceId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@ResourceId", resourceId);
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            userResource = new InventoryResource(
                                id: (Guid)reader["Id"],
                                userId: (Guid)reader["UserId"],
                                resourceId: (Guid)reader["ResourceId"],
                                quantity: (int)reader["Quantity"]);
                        }
                    }
                }
            }
            return userResource;
        }

        public async Task AddUserResourceAsync(InventoryResource userResource)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "INSERT INTO InventoryResources (Id, UserId, ResourceId, Quantity) VALUES (@Id, @UserId, @ResourceId, @Quantity)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", userResource.Id);
                    command.Parameters.AddWithValue("@UserId", userResource.UserId);
                    command.Parameters.AddWithValue("@ResourceId", userResource.ResourceId);
                    command.Parameters.AddWithValue("@Quantity", userResource.Quantity);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateUserResourceAsync(InventoryResource userResource)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "UPDATE InventoryResources SET Quantity = @Quantity WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", userResource.Id);
                    command.Parameters.AddWithValue("@Quantity", userResource.Quantity);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteUserResourceAsync(Guid userResourceId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "DELETE FROM InventoryResources WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", userResourceId);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
