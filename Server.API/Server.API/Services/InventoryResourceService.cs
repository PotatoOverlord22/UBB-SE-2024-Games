using Microsoft.EntityFrameworkCore;
using Server.API.Models;
using Server.API.Utils;

namespace Server.API.Services
{
    public class InventoryResourceService : IInventoryResourceService
    {
        private readonly GamesContext context;

        public InventoryResourceService(GamesContext context)
        {
            this.context = context;
        }

        public async Task AddUserResourceAsync(InventoryResource userResource)
        {
            context.InventoryResources.Add(userResource);
            await context.SaveChangesAsync();
        }

        // Does this delete one resource or all resources for the user?
        public async Task DeleteUserResourceAsync(Guid userResourceId)
        {
            var userResource = context.InventoryResources.Find(userResourceId) ?? throw new KeyNotFoundException("Inventory resource not found");
            context.InventoryResources.Remove(userResource);
            await context.SaveChangesAsync();
        }

        public async Task<InventoryResource> GetUserResourceByResourceIdAsync(Guid userId, Guid resourceId)
        {
            var userResource = await context.InventoryResources
                                     .FirstOrDefaultAsync(ir => ir.Owner.Id == userId && ir.Resource.Id == resourceId) ?? throw new KeyNotFoundException("Resource not found");
            return userResource;
        }

        public async Task<List<InventoryResource>> GetUserResourcesAsync(Guid userId)
        {
            return await context.InventoryResources
                        .Where(ir => ir.Owner.Id == userId)
                        .ToListAsync();
        }

        public async Task UpdateUserResourceAsync(InventoryResource userResource)
        {
            if (context.InventoryResources.Find(userResource.Id) == null)
            {
                throw new KeyNotFoundException("Inventory Resource not found");
            }

            context.InventoryResources.Update(userResource);
            await context.SaveChangesAsync();
        }
    }
}
