using GameWorld.Entities;

namespace GameWorld.Services
{
    public interface IResourceService
    {
        Task<Resource> GetResourceByIdAsync(Guid resourceId);
        Task<List<Resource>> GetAllResourcesAsync();
    }
}
