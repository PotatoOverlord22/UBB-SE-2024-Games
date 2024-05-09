using System.Data;
using HarvestHaven.Entities;
using HarvestHaven.Utils;

namespace HarvestHaven.Repositories
{
    public class InventoryResourceRepository : IInventoryResourceRepository
    {
        private readonly IDatabaseProvider databaseProvider;

        public InventoryResourceRepository(IDatabaseProvider databaseProvider)
        {
            this.databaseProvider = databaseProvider;
        }

        public async Task<List<InventoryResource>> GetUserResourcesAsync(Guid userId)
        {
            List<InventoryResource> userResources = new List<InventoryResource>();
            var parameters = new Dictionary<string, object> { { "@UserId", userId } };

            using (IDataReader reader = await databaseProvider.ExecuteReaderAsync("SELECT * FROM InventoryResources WHERE UserId = @UserId", parameters))
            {
                int idOrdinal = reader.GetOrdinal("Id");
                int userIdOrdinal = reader.GetOrdinal("UserId");
                int resourceIdOrdinal = reader.GetOrdinal("ResourceId");
                int quantityOrdinal = reader.GetOrdinal("Quantity");

                while (reader.Read())
                {
                    userResources.Add(new InventoryResource(
                        id: reader.GetGuid(idOrdinal),
                        userId: reader.GetGuid(userIdOrdinal),
                        resourceId: reader.GetGuid(resourceIdOrdinal),
                        quantity: reader.GetInt32(quantityOrdinal)));
                }
            }
            return userResources;
        }

        public async Task<InventoryResource> GetUserResourceByResourceIdAsync(Guid userId, Guid resourceId)
        {
            InventoryResource? userResource = null;
            var parameters = new Dictionary<string, object>
            {
                { "@UserId", userId },
                { "@ResourceId", resourceId }
            };

            using (IDataReader reader = await databaseProvider.ExecuteReaderAsync("SELECT * FROM InventoryResources WHERE UserId = @UserId AND ResourceId = @ResourceId", parameters))
            {
                int idOrdinal = reader.GetOrdinal("Id");
                int userIdOrdinal = reader.GetOrdinal("UserId");
                int resourceIdOrdinal = reader.GetOrdinal("ResourceId");
                int quantityOrdinal = reader.GetOrdinal("Quantity");

                if (reader.Read())
                {
                    userResource = new InventoryResource(
                        id: reader.GetGuid(idOrdinal),
                        userId: reader.GetGuid(userIdOrdinal),
                        resourceId: reader.GetGuid(resourceIdOrdinal),
                        quantity: reader.GetInt32(quantityOrdinal));
                }
            }
            return userResource;
        }

        public async Task AddUserResourceAsync(InventoryResource userResource)
        {
            var parameters = new Dictionary<string, object>
            {
                { "@Id", userResource.Id },
                { "@UserId", userResource.UserId },
                { "@ResourceId", userResource.ResourceId },
                { "@Quantity", userResource.Quantity }
            };

            await databaseProvider.ExecuteReaderAsync(
                "INSERT INTO InventoryResources (Id, UserId, ResourceId, Quantity) VALUES (@Id, @UserId, @ResourceId, @Quantity)",
                parameters);
        }

        public async Task UpdateUserResourceAsync(InventoryResource userResource)
        {
            var parameters = new Dictionary<string, object>
            {
                { "@Id", userResource.Id },
                { "@Quantity", userResource.Quantity }
            };

            await databaseProvider.ExecuteReaderAsync(
                "UPDATE InventoryResources SET Quantity = @Quantity WHERE Id = @Id",
                parameters);
        }

        public async Task DeleteUserResourceAsync(Guid userResourceId)
        {
            var parameters = new Dictionary<string, object> { { "@Id", userResourceId } };

            await databaseProvider.ExecuteReaderAsync("DELETE FROM InventoryResources WHERE Id = @Id", parameters);
        }
    }
}
