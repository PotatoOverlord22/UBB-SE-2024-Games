using Microsoft.Data.SqlClient;
using HarvestHaven.Utils;
using HarvestHaven.Entities;

namespace HarvestHaven.Repositories
{
    public static class ResourceRepository
    {
        private static readonly string _connectionString = DatabaseHelper.GetDatabaseFilePath();

        public static async Task<List<Resource>> GetAllResourcesAsync()
        {
            List<Resource> resources = new List<Resource>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Resources", connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            resources.Add(new Resource
                            (
                                id: (Guid)reader["Id"],
                                resourceType: ((string)reader["ResourceType"]).ToEnum<ResourceType>()
                            ));
                        }
                    }
                }
            }
            return resources;
        }

        public static async Task<Resource> GetResourceByIdAsync(Guid resourceId)
        {
            Resource resource = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT * FROM Resources WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", resourceId);
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            resource = new Resource
                            (
                                id: (Guid)reader["Id"],
                                resourceType: ((string)reader["ResourceType"]).ToEnum<ResourceType>()
                            );
                        }
                    }
                }
            }
            return resource;
        }

        public static async Task<Resource> GetResourceByTypeAsync(ResourceType resourceType)
        {
            Resource resource = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT * FROM Resources WHERE ResourceType = @ResourceType";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ResourceType", resourceType.ToString());
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            resource = new Resource
                            (
                                id: (Guid)reader["Id"],
                                resourceType: ((string)reader["ResourceType"]).ToEnum<ResourceType>()
                            );
                        }
                    }
                }
            }
            return resource;
        }


        public static async Task AddResourceAsync(Resource resource)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "INSERT INTO Resources (Id, ResourceType) VALUES (@Id, @ResourceType)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", resource.Id);
                    command.Parameters.AddWithValue("@ResourceType", resource.ResourceType.ToString());
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public static async Task UpdateResourceAsync(Resource resource)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "UPDATE Resources SET ResourceType = @ResourceType WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", resource.Id);
                    command.Parameters.AddWithValue("@ResourceType", resource.ResourceType.ToString());
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public static async Task DeleteResourceAsync(Guid resourceId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "DELETE FROM Resources WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", resourceId);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
