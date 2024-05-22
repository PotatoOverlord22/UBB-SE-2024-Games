using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using GameWorld.Resources.Utils;
using GameWorld.Models;
using Newtonsoft.Json;

namespace GameWorld.Repositories
{
    public class ResourceRepository : IResourceRepository
    {
        // private readonly string connectionString = DatabaseHelper.GetDatabaseFilePath();
        private readonly HttpClient httpClient;
        private readonly string baseURL;

        public ResourceRepository(HttpClient httpClient, string baseURL)
        {
            this.httpClient = httpClient;
            this.baseURL = baseURL;
        }

        public async Task<List<Resource>> GetAllResourcesAsync()
        {
            // Nu stiu ce fac cu asta daca nu va place se sterge
            /*List<Resource> resources = new List<Resource>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Resources", connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            resources.Add(new Resource(
                                id: (Guid)reader["Id"],
                                resourceType: ((string)reader["ResourceType"]).ToEnum<ResourceType>()));
                        }
                    }
                }
            }
            return resources;*/

            try
            {
                // Send a GET request to the base URL (localhost:3000 ?)
                var response = await httpClient.GetAsync($"{baseURL}/resources");
                // Make sure the response is good otherwise throw error
                response.EnsureSuccessStatusCode();
                // Turn the HTTPContent into a json string
                string responseContent = await response.Content.ReadAsStringAsync();
                // Turn string to List
                // Aici arunc o eroare da se poate returna si o lista goala. Cum crezi ca ii mai bine
                var resources = JsonConvert.DeserializeObject<List<Resource>>(responseContent) ?? throw new Exception("Response content from getting all resources from the backend is invalid: ");
                return resources;
            }
            catch (Exception exception)
            {
                throw new Exception("Error on getting all resources from the Server: " + exception.Message);
            }
        }

        public async Task<Resource> GetResourceByIdAsync(Guid resourceId)
        {
            // La fel si aici
            /*Resource resource = null;
            /*using (SqlConnection connection = new SqlConnection(connectionString))
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
                            resource = new Resource(
                                id: (Guid)reader["Id"],
                                resourceType: ((string)reader["ResourceType"]).ToEnum<ResourceType>());
                        }
                    }
                }
            }*/
            // return resource;
            try
            {
                // Aici se poate face si POST daca se considera ca nu e okay sa expui IDu in URI
                var response = await httpClient.GetAsync($"{baseURL}/resources/{resourceId}");
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                var resources = JsonConvert.DeserializeObject<Resource>(responseContent) ?? throw new Exception("Response content from getting resource by ID from the backend is invalid: ");
                return resources;
            }
            catch (Exception exception)
            {
                throw new Exception("Error on getting resource by ID from the Server: " + exception.Message);
            }
        }

        public async Task<Resource> GetResourceByTypeAsync(ResourceType resourceType)
        {
            /*Resource resource = null;
           /* using (SqlConnection connection = new SqlConnection(connectionString))
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
                            resource = new Resource(
                                id: (Guid)reader["Id"],
                                resourceType: ((string)reader["ResourceType"]).ToEnum<ResourceType>());
                        }
                    }
                }
            }*/
            // return resource;
            try
            {
                var response = await httpClient.GetAsync($"{baseURL}/resources/type/{resourceType}");
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                var resources = JsonConvert.DeserializeObject<Resource>(responseContent) ?? throw new Exception("Response content from getting resource by Type from the backend is invalid: ");
                return resources;
            }
            catch (Exception exception)
            {
                throw new Exception("Error on getting resource by Type from the Server: " + exception.Message);
            }
        }

        public async Task AddResourceAsync(Resource resource)
        {
            /* using (SqlConnection connection = new SqlConnection(connectionString))
             {
                 await connection.OpenAsync();
                 string query = "INSERT INTO Resources (Id, ResourceType) VALUES (@Id, @ResourceType)";
                 using (SqlCommand command = new SqlCommand(query, connection))
                 {
                     command.Parameters.AddWithValue("@Id", resource.Id);
                     command.Parameters.AddWithValue("@ResourceType", resource.ResourceType.ToString());
                     await command.ExecuteNonQueryAsync();
                 }
             }*/
            try
            {
                var sentContent = JsonConvert.SerializeObject(resource);
                var content = new StringContent(sentContent, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync($"{baseURL}/resources", content);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception exception)
            {
                throw new Exception("Error on sending resource to the Server: " + exception.Message);
            }
        }

        public async Task UpdateResourceAsync(Resource resource)
        {
            /* using (SqlConnection connection = new SqlConnection(connectionString))
             {
                 await connection.OpenAsync();
                 string query = "UPDATE Resources SET ResourceType = @ResourceType WHERE Id = @Id";
                 using (SqlCommand command = new SqlCommand(query, connection))
                 {
                     command.Parameters.AddWithValue("@Id", resource.Id);
                     command.Parameters.AddWithValue("@ResourceType", resource.ResourceType.ToString());
                     await command.ExecuteNonQueryAsync();
                 }
             }*/
            try
            {
                var putContent = JsonConvert.SerializeObject(resource);
                var content = new StringContent(putContent, Encoding.UTF8, "application/json");

                var response = await httpClient.PutAsync($"{baseURL}/resources/{resource.Id}", content);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception exception)
            {
                throw new Exception("Error on putting resource to the Server: " + exception.Message);
            }
        }

        public async Task DeleteResourceAsync(Guid resourceId)
        {
            /* using (SqlConnection connection = new SqlConnection(connectionString))
             {
                 await connection.OpenAsync();
                 string query = "DELETE FROM Resources WHERE Id = @Id";
                 using (SqlCommand command = new SqlCommand(query, connection))
                 {
                     command.Parameters.AddWithValue("@Id", resourceId);
                     await command.ExecuteNonQueryAsync();
                 }
             }*/
            try
            {
                var response = await httpClient.DeleteAsync($"{baseURL}/resources/{resourceId}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception exception)
            {
                throw new Exception("Error on DELETE resource to the Server: " + exception.Message);
            }
        }
    }
}
