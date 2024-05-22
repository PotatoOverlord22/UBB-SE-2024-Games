using Microsoft.Data.SqlClient;
using GameWorld.Resources.Utils;
using GameWorld.Models;

namespace GameWorld.Repositories
{
    public class MarketSellResourceRepository : IMarketSellResourceRepository
    {
        private readonly string connectionString = DatabaseHelper.GetDatabaseFilePath();

        public async Task<List<MarketSellResource>> GetAllSellResourcesAsync()
        {
            List<MarketSellResource> sellResources = new List<MarketSellResource>();
           /* using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("SELECT * FROM MarketSellResources", connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            sellResources.Add(new MarketSellResource(
                                id: (Guid)reader["Id"],
                                resourceId: (Guid)reader["ResourceId"],
                                sellPrice: (int)reader["SellPrice"]));
                        }
                    }
                }
            }*/
            return sellResources;
        }

        public async Task<MarketSellResource> GetMarketSellResourceByResourceIdAsync(Guid resourceId)
        {
            MarketSellResource? sellResource = null;

           /* using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("SELECT * FROM MarketSellResources WHERE ResourceId = @ResourceId", connection))
                {
                    command.Parameters.AddWithValue("@ResourceId", resourceId);
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            sellResource = new MarketSellResource(
                                id: (Guid)reader["Id"],
                                resourceId: (Guid)reader["ResourceId"],
                                sellPrice: (int)reader["SellPrice"]);
                        }
                    }
                }
            }*/

            return sellResource;
        }

        public async Task AddMarketSellResourceAsync(MarketSellResource marketSellResource)
        {
           /* using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "INSERT INTO MarketSellResources (Id, ResourceId, SellPrice) VALUES (@Id, @ResourceId, @SellPrice)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", marketSellResource.Id);
                    command.Parameters.AddWithValue("@ResourceId", marketSellResource.ResourceToSellId);
                    command.Parameters.AddWithValue("@SellPrice", marketSellResource.SellPrice);
                    await command.ExecuteNonQueryAsync();
                }
            }*/
        }

        public async Task UpdateMarketSellResourceAsync(MarketSellResource marketSellResource)
        {
           /* using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "UPDATE MarketSellResources SET ResourceId = @ResourceId, SellPrice = @SellPrice WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", marketSellResource.Id);
                    command.Parameters.AddWithValue("@ResourceId", marketSellResource.ResourceToSellId);
                    command.Parameters.AddWithValue("@SellPrice", marketSellResource.SellPrice);
                    await command.ExecuteNonQueryAsync();
                }
            }*/
        }

        public async Task DeleteMarketSellResourceAsync(Guid marketSellResourceId)
        {
            /*using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "DELETE FROM MarketSellResources WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", marketSellResourceId);
                    await command.ExecuteNonQueryAsync();
                }
            }*/
        }
    }
}
