using GameWorldClassLibrary.Models;
using GameWorldClassLibrary.Repositories;
using GameWorldClassLibrary.Utils;

namespace GameWorldClassLibrary.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IInventoryResourceRepository inventoryResourceRepository;
        private readonly IResourceRepository resourceRepository;
        private readonly ICommentRepository commentRepository;

        private const int FIRST_USER_RANK = 1;

        public UserService(IUserRepository userRepository, IInventoryResourceRepository inventoryResourceRepository, IResourceRepository resourceRepository, ICommentRepository commentRepository)
        {
            this.userRepository = userRepository;
            this.inventoryResourceRepository = inventoryResourceRepository;
            this.resourceRepository = resourceRepository;
            this.commentRepository = commentRepository;
        }
        public async Task<User> GetUserByIdAsync(Guid userId)
        {
            return await userRepository.GetUserByIdAsync(userId);
        }

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
                Resource resourceOfUser = await resourceRepository.GetResourceByIdAsync(inventoryResource.Resource.Id);
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

        public bool IsTradeHallUnlocked()
        {
            // Throw an exception if the user is not logged in.
            if (GameStateManager.GetCurrentUser() == null)
            {
                throw new Exception("User must be logged in!");
            }

            return DateTime.UtcNow - GameStateManager.GetCurrentUser().TradeHallUnlockTime < TimeSpan.FromDays(Constants.TRADEHALL_LIFETIME_IN_DAYS);
        }

        public async Task UnlockTradeHall()
        {
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

            // Decrease the user coins and the trade hall unlock time.
            User newUser = GameStateManager.GetCurrentUser();
            newUser.Coins -= Constants.TRADEHALL_UNLOCK_PRICE;
            newUser.TradeHallUnlockTime = DateTime.UtcNow;
            await userRepository.UpdateUserAsync(newUser);
            GameStateManager.SetCurrentUser(newUser);
        }
        public async Task AddCommentForAnotherUser(User targetUser, string message)
        {
            // Throw an exception if the user is not logged in.
            if (GameStateManager.GetCurrentUser() == null)
            {
                throw new Exception("User must be logged in!");
            }

            await commentRepository.CreateCommentAsync(new Comment(
               id: Guid.NewGuid(),
               poster: targetUser,
               commentMessage: message,
               creationTime: DateTime.UtcNow));
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
        public async Task<List<User>> GetAllUsersSortedByCoinsAsync()
        {
            // Get a list with all the users from the database.
            List<User> users = await userRepository.GetAllUsersAsync();

            // Sort the users by coins.
            users.Sort((user1, user2) => user2.Coins.CompareTo(user1.Coins));

            return users;
        }

        public List<string> GetAllRequestsByToUserID(Guid toUser)
        {
            return new List<string>();
        }

        public List<Tuple<Guid, Guid>> GetAllRequestsByToUserIDSimplified(Guid toUser)
        {
            return new List<Tuple<Guid, Guid>>();
        }

        public int GetChipsByUserId(Guid userId)
        {
            var user = userRepository.GetUserByIdAsync(userId).Result;
            if (user == null)
            {
                throw new Exception($"User with id: {userId} not found in the database.");
            }
            return user.UserChips;
        }

        public List<string> GetLeaderboard()
        {
            List<string> leaderboardAsString = new List<string>();
            List<User> leaderboard = userRepository.GetPokerLeaderboard().Result;
            int rank = FIRST_USER_RANK;
            leaderboard.Reverse();
            foreach (User user in leaderboard)
            {
                leaderboardAsString.Add($"{rank}. {user.Username} - Lvl: {user.UserLevel} - Chips: {user.UserChips}");
                rank++;
            }
            return leaderboardAsString;
        }

        public void UpdateUserChips(Guid id, int userChips)
        {
            // TODO validation
            userRepository.UpdateUserChipsAsync(id, userChips);
        }

        public void UpdateUserStreak(Guid id, int userStreak)
        {
            // TODO validation
            userRepository.UpdateUserStreak(id, userStreak);
        }

        public void UpdateUserLastLogin(Guid id, DateTime now)
        {
            // TODO Validation
            userRepository.UpdateUserLastLogin(id, now);
        }

        public void UpdateUserStack(Guid id, int userStack)
        {
            // TODO validation
            userRepository.UpdateUserStack(id, userStack);
        }
    }
}
