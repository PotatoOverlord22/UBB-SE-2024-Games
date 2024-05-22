using GameWorld.Models;

namespace GameWorld.Services
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(Guid userId);
        Task<Dictionary<InventoryResource, Resource>> GetInventoryResources(Guid userId);
        Task<InventoryResource> GetInventoryResourceByType(ResourceType resourceType, Guid userId);
        bool IsTradeHallUnlocked();
        Task UnlockTradeHall();
        Task AddCommentForAnotherUser(Guid targetUserId, string message);
        Task<List<Comment>> GetMyComments();
        Task DeleteComment(Guid commentId);
        Task<List<User>> GetAllUsersSortedByCoinsAsync();
    }
}
