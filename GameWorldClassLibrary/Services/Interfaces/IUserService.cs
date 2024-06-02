using GameWorldClassLibrary.Models;

namespace GameWorldClassLibrary.Services
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(Guid userId);
        Task<Dictionary<InventoryResource, Resource>> GetInventoryResources(Guid userId);
        Task<InventoryResource> GetInventoryResourceByType(ResourceType resourceType, Guid userId);
        bool IsTradeHallUnlocked();
        Task UnlockTradeHall();
        Task AddCommentForAnotherUser(User targetUser, string message);
        Task<List<Comment>> GetMyComments();
        Task DeleteComment(Guid commentId);
        Task<List<User>> GetAllUsersSortedByCoinsAsync();
        List<string> GetAllRequestsByToUserID(Guid toUser);
        List<Tuple<Guid, Guid>> GetAllRequestsByToUserIDSimplified(Guid toUser);
        void UpdateUserLastLogin(Guid id, DateTime now);
        Task<User> GetUserByUsername(string v);
    }
}
