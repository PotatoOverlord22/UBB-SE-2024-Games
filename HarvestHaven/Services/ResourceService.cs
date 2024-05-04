using HarvestHaven.Repositories;
using HarvestHaven.Entities;

namespace HarvestHaven.Services
{
    public static class ResourceService
    {
        public static async Task<Resource> GetResourceByIdAsync(Guid resourceId)
        {
            return await ResourceRepository.GetResourceByIdAsync(resourceId);
        }
        public static async Task<List<Resource>> GetAllResourcesAsync()
        {
            return await ResourceRepository.GetAllResourcesAsync();
        }
    }
}
