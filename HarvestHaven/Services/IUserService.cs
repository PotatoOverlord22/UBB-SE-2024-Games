using HarvestHaven.Entities;

namespace HarvestHaven.Services
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(Guid userId);
        Task<Dictionary<InventoryResource, Resource>> GetInventoryResources();
        Task<InventoryResource> GetInventoryResourceByType(ResourceType resourceType);
        Task UpdateUserWater();
        bool IsTradeHallUnlocked();
        Task UnlockTradeHall();
        Task AddCommentForAnotherUser(Guid targetUserId, string message);
        Task<List<Comment>> GetMyComments();
        Task DeleteComment(Guid commentId);
        Task<List<User>> GetAllUsersSortedByCoinsAsync();
    }
}
