using GameWorldClassLibrary.Models;

namespace GameWorldClassLibrary.Services.Interfaces
{
    public interface IResourceService
    {
        Task<Resource> GetResourceByIdAsync(Guid resourceId);
        Task<List<Resource>> GetAllResourcesAsync();
    }
}
