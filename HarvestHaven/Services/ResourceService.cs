using HarvestHaven.Repositories;
using HarvestHaven.Entities;

namespace HarvestHaven.Services
{
    public class ResourceService : IResourceService
    {
        public async Task<Resource> GetResourceByIdAsync(Guid resourceId)
        {
            return await ResourceRepository.GetResourceByIdAsync(resourceId);
        }
        public async Task<List<Resource>> GetAllResourcesAsync()
        {
            return await ResourceRepository.GetAllResourcesAsync();
        }
    }
}
