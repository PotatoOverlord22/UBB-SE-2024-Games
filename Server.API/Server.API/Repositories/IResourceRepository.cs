using Server.API.Models;

namespace Server.API.Services
{
    public interface IResourceRepository
    {
        Task<List<Resource>> GetAllResourcesAsync();
        Task<Resource> GetResourceByIdAsync(Guid resourceId);
        Task<Resource> GetResourceByTypeAsync(ResourceType resourceType);
        Task AddResourceAsync(Resource resource);
        Task UpdateResourceAsync(Resource resource);
        Task DeleteResourceAsync(Guid resourceId);
    }
}
