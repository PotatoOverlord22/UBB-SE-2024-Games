using HarvestHaven.Entities;

namespace HarvestHaven.Services
{
    public interface IResourceService
    {
        Task<Resource> GetResourceByIdAsync(Guid resourceId);
        Task<List<Resource>> GetAllResourcesAsync();
    }
}
