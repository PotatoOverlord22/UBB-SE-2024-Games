using Server.API.Models;

namespace Server.API.Services
{
    public interface IInventoryResourceService
    {
        Task<List<InventoryResource>> GetUserResourcesAsync(Guid userId);
        Task<InventoryResource> GetUserResourceByResourceIdAsync(Guid userId, Guid resourceId);
        Task AddUserResourceAsync(InventoryResource userResource);
        Task UpdateUserResourceAsync(InventoryResource userResource);
        Task DeleteUserResourceAsync(Guid userResourceId);
    }
}
