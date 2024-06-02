using GameWorldClassLibrary.Models;
using GameWorldClassLibrary.Repositories;

namespace GameWorld.Services
{
    public class ResourceService : IResourceService
    {
        private readonly IResourceRepository resourceRepository;
        public ResourceService(IResourceRepository resourceRepository)
        {
            this.resourceRepository = resourceRepository;
        }
        public async Task<Resource> GetResourceByIdAsync(Guid resourceId)
        {
            return await resourceRepository.GetResourceByIdAsync(resourceId);
        }
        public async Task<List<Resource>> GetAllResourcesAsync()
        {
            return await resourceRepository.GetAllResourcesAsync();
        }
    }
}
