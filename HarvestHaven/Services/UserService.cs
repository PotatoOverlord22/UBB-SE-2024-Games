using HarvestHaven.Entities;
using HarvestHaven.Repositories;
using HarvestHaven.Utils;

namespace HarvestHaven.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IInventoryResourceRepository inventoryResourceRepository;
        private readonly IResourceRepository resourceRepository;
        private readonly ICommentRepository commentRepository;

        public UserService(IUserRepository userRepository, IInventoryResourceRepository inventoryResourceRepository, IResourceRepository resourceRepository, ICommentRepository commentRepository)
        {
            this.userRepository = userRepository;
            this.inventoryResourceRepository = inventoryResourceRepository;
            this.resourceRepository = resourceRepository;
            this.commentRepository = commentRepository;
        }
        #region Authentification
        public async Task<User> GetUserByIdAsync(Guid userId)
        {
            return await userRepository.GetUserByIdAsync(userId);
        }
        #endregion

        #region Inventory

        public async Task<Dictionary<InventoryResource, Resource>> GetInventoryResources(Guid userId)
        {
            // Get all the inventory resources for the current user from the database.
            List<InventoryResource> inventoryResources = await inventoryResourceRepository.GetUserResourcesAsync(userId);

            // Initialize the dictionary that will be returned.
            Dictionary<InventoryResource, Resource> inventoryResourcesMap = new Dictionary<InventoryResource, Resource>();

            // Go through each inventory resource.
            foreach (InventoryResource inventoryResource in inventoryResources)
            {
                // Get the corresponding resource from the database.
                Resource resourceOfUser = await resourceRepository.GetResourceByIdAsync(inventoryResource.ResourceId);
                if (resourceOfUser == null)
                {
                    throw new Exception($"No corresponding resource found for the inventory resource with id: {inventoryResource.Id}");
                }

                // Add the pair in the dictionary.
                inventoryResourcesMap.Add(inventoryResource, resourceOfUser);
            }

            return inventoryResourcesMap;
        }

        public async Task<InventoryResource> GetInventoryResourceByType(ResourceType resourceType, Guid userId)
        {
            // Throw an exception if the user is not logged in.
            if (GameStateManager.GetCurrentUser() == null)
            {
                throw new Exception("User must be logged in!");
            }

            // Get the resource with the given type from the database.
            Resource resource = await resourceRepository.GetResourceByTypeAsync(resourceType);
            if (resource == null)
            {
                throw new Exception($"Resource with type: {resourceType.ToString()} found in the database.");
            }

            // Get the inventory resource from the database.
            InventoryResource inventoryResource = await inventoryResourceRepository.GetUserResourceByResourceIdAsync(userId, resource.Id);

            return inventoryResource;
        }

        #endregion

        #region TradeHall

        public bool IsTradeHallUnlocked()
        {
            // Throw an exception if the user is not logged in.
            if (GameStateManager.GetCurrentUser() == null)
            {
                throw new Exception("User must be logged in!");
            }

            return (DateTime.UtcNow - GameStateManager.GetCurrentUser().TradeHallUnlockTime) < TimeSpan.FromDays(Constants.TRADEHALL_LIFETIME_IN_DAYS);
        }

        public async Task UnlockTradeHall()
        {
            #region Validation
            // Throw an exception if the user is not logged in.
            if (GameStateManager.GetCurrentUser() == null)
            {
                throw new Exception("User must be logged in!");
            }

            // Throw an exception in case the trade hall is already unlocked.
            if (DateTime.UtcNow - GameStateManager.GetCurrentUser().TradeHallUnlockTime < TimeSpan.FromDays(Constants.TRADEHALL_LIFETIME_IN_DAYS))
            {
                throw new Exception("Trade hall already unlocked!");
            }

            // Throw an exception if the user does not have enough coins to unlock the trade hall.
            if (GameStateManager.GetCurrentUser().Coins < Constants.TRADEHALL_UNLOCK_PRICE)
            {
                throw new Exception("You don't have enough coins to unlock the trade hall.");
            }
            #endregion

            // Decrease the user coins and the trade hall unlock time.
            User newUser = GameStateManager.GetCurrentUser();
            newUser.Coins -= Constants.TRADEHALL_UNLOCK_PRICE;
            newUser.TradeHallUnlockTime = DateTime.UtcNow;
            await userRepository.UpdateUserAsync(newUser);
            GameStateManager.SetCurrentUser(newUser);
        }

        #endregion

        #region Comments
        public async Task AddCommentForAnotherUser(Guid targetUserId, string message)
        {
            // Throw an exception if the user is not logged in.
            if (GameStateManager.GetCurrentUser() == null)
            {
                throw new Exception("User must be logged in!");
            }

            await commentRepository.CreateCommentAsync(new Comment(
               id: Guid.NewGuid(),
               posterUserId: targetUserId,
               commentMessage: message,
               commentCreationTime: DateTime.UtcNow));
        }

        public async Task<List<Comment>> GetMyComments()
        {
            // Throw an exception if the user is not logged in.
            if (GameStateManager.GetCurrentUser() == null)
            {
                throw new Exception("User must be logged in!");
            }

            return await commentRepository.GetUserCommentsAsync(GameStateManager.GetCurrentUserId());
        }

        public async Task DeleteComment(Guid commentId)
        {
            // Throw an exception if the user is not logged in.
            if (GameStateManager.GetCurrentUser() == null)
            {
                throw new Exception("User must be logged in!");
            }

            await commentRepository.DeleteCommentAsync(commentId);
        }

        #endregion

        #region Leaderboard
        public async Task<List<User>> GetAllUsersSortedByCoinsAsync()
        {
            // Get a list with all the users from the database.
            List<User> users = await userRepository.GetAllUsersAsync();

            // Sort the users by coins.
            users.Sort((user1, user2) => user2.Coins.CompareTo(user1.Coins));

            return users;
        }

        #endregion
    }
}
