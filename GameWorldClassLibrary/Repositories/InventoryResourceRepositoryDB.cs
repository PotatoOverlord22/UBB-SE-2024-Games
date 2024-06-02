using Microsoft.EntityFrameworkCore;
using GameWorldClassLibrary.Models;
using GameWorldClassLibrary.Utils;

namespace GameWorldClassLibrary.Repositories
{
    public class InventoryResourceRepositoryDB : IInventoryResourceRepository
    {
        private readonly GamesContext context;

        public InventoryResourceRepositoryDB(GamesContext context)
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
