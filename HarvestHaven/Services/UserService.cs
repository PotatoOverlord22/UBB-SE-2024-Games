using HarvestHaven.Entities;
using HarvestHaven.Repositories;
using HarvestHaven.Utils;

namespace HarvestHaven.Services
{
    public static class UserService
    {
        #region Authentification
        public static async Task<User> GetUserByIdAsync(Guid userId)
        {
            return await UserRepository.GetUserByIdAsync(userId);
        }
        #endregion

        #region Inventory

        public static async Task<Dictionary<InventoryResource, Resource>> GetInventoryResources()
        {
            // Throw an exception if the user is not logged in.
            if (GameStateManager.GetCurrentUser() == null) throw new Exception("User must be logged in!");

            // Get all the inventory resources for the current user from the database.
            List<InventoryResource> inventoryResources = await InventoryResourceRepository.GetUserResourcesAsync(GameStateManager.GetCurrentUserId());

            // Initialize the dictionary that will be returned.
            Dictionary<InventoryResource, Resource> inventoryResourcesMap = new Dictionary<InventoryResource, Resource>();


            // Go through each inventory resource.
            foreach(InventoryResource inventoryResource in inventoryResources)
            {
                // Get the corresponding resource from the database.
                Resource resource = await ResourceRepository.GetResourceByIdAsync(inventoryResource.ResourceId);
                if (resource == null) throw new Exception($"No corresponding resource found for the inventory resource with id: {inventoryResource.Id}");
            
                // Add the pair in the dictionary.
                inventoryResourcesMap.Add(inventoryResource, resource);
            }

            return inventoryResourcesMap;
        }

        public static async Task<InventoryResource> GetInventoryResourceByType(ResourceType resourceType)
        {
            // Throw an exception if the user is not logged in.
            if (GameStateManager.GetCurrentUser() == null) throw new Exception("User must be logged in!");

            // Get the resource with the given type from the database.
            Resource resource = await ResourceRepository.GetResourceByTypeAsync(resourceType);
            if (resource == null) throw new Exception($"Resource with type: {resourceType.ToString()} found in the database.");

            // Get the inventory resource from the database.
            InventoryResource inventoryResource = await InventoryResourceRepository.GetUserResourceByResourceIdAsync(GameStateManager.GetCurrentUserId(), resource.Id);

            return inventoryResource;
        }

        public static async Task UpdateUserWater()
        {
            // Throw an exception if the user is not logged in.
            if (GameStateManager.GetCurrentUser() == null) throw new Exception("User must be logged in!");

            // Store the time passed between the current time and the last time the user received water.
            TimeSpan? timePassed = DateTime.UtcNow - GameStateManager.GetCurrentUser().LastTimeReceivedWater;

            // If the user did not receive water yet we assume that he needs to receive water for one interval.
            if (timePassed == null) timePassed = TimeSpan.FromHours(Constants.WATER_INTERVAL_IN_HOURS);

            // Return in case the user received water in less than an hour.
            if (timePassed.Value.TotalHours < Constants.WATER_INTERVAL_IN_HOURS) return;

            // Compute the quantity of water needed for the user.
            int waterQuantity = (int)timePassed.Value.TotalHours * Constants.WATER_PER_INTERVAL;
 
            // Get the water resource reference from the database.
            Resource waterResource = await ResourceRepository.GetResourceByTypeAsync(ResourceType.Water);
            if (waterResource == null) throw new Exception("Water resource not found in the database.");

            // Get the user's water from the inventory.
            InventoryResource waterInventoryResource = await GetInventoryResourceByType(ResourceType.Water);
            
            // If the water resource exists in the inventory simply update the quantity.
            if(waterInventoryResource != null)
            {
                waterInventoryResource.Quantity += waterQuantity;
                await InventoryResourceRepository.UpdateUserResourceAsync(waterInventoryResource);
            }
            else // Otherwise add the resource to the inventory.
            {
                await InventoryResourceRepository.AddUserResourceAsync(
                    new InventoryResource(
                        id: Guid.NewGuid(),
                        userId: GameStateManager.GetCurrentUserId(),
                        resourceId: waterResource.Id,
                        quantity: waterQuantity));
            }


            // Update last time received water to current time.
            User newUser = GameStateManager.GetCurrentUser();
            newUser.LastTimeReceivedWater = DateTime.UtcNow;
            await UserRepository.UpdateUserAsync(newUser);
            GameStateManager.SetCurrentUser(newUser);               
        }

        #endregion

        #region TradeHall

        public static bool IsTradeHallUnlocked()
        {
            // Throw an exception if the user is not logged in.
            if (GameStateManager.GetCurrentUser() == null) throw new Exception("User must be logged in!");
            return (DateTime.UtcNow - GameStateManager.GetCurrentUser().TradeHallUnlockTime) < TimeSpan.FromDays(Constants.TRADEHALL_LIFETIME_IN_DAYS);
        } 

        public static async Task UnlockTradeHall()
        {
            #region Validation
            // Throw an exception if the user is not logged in.
            if (GameStateManager.GetCurrentUser() == null) throw new Exception("User must be logged in!");

            // Throw an exception in case the trade hall is already unlocked.
            if (DateTime.UtcNow - GameStateManager.GetCurrentUser().TradeHallUnlockTime < TimeSpan.FromDays(Constants.TRADEHALL_LIFETIME_IN_DAYS)) throw new Exception("Trade hall already unlocked!");

            // Throw an exception if the user does not have enough coins to unlock the trade hall.
            if (GameStateManager.GetCurrentUser().Coins < Constants.TRADEHALL_UNLOCK_PRICE) throw new Exception("You don't have enough coins to unlock the trade hall.");
            #endregion

            // Decrease the user coins and the trade hall unlock time.
            User newUser = GameStateManager.GetCurrentUser();
            newUser.Coins -= Constants.TRADEHALL_UNLOCK_PRICE;
            newUser.TradeHallUnlockTime = DateTime.UtcNow;
            await UserRepository.UpdateUserAsync(newUser);
            GameStateManager.SetCurrentUser(newUser);     
        }

        #endregion

        #region Comments
        public static async Task AddCommentForAnotherUser(Guid targetUserId, string message)
        {
            // Throw an exception if the user is not logged in.
            if (GameStateManager.GetCurrentUser() == null) throw new Exception("User must be logged in!");
            
            await CommentRepository.CreateCommentAsync(new Comment(
               id: Guid.NewGuid(),
               userId: targetUserId,
               message: message,
               createdTime: DateTime.UtcNow
            ));
        }

        public static async Task<List<Comment>> GetMyComments()
        {
            // Throw an exception if the user is not logged in.
            if (GameStateManager.GetCurrentUser() == null) throw new Exception("User must be logged in!");
            
            return await CommentRepository.GetUserCommentsAsync(GameStateManager.GetCurrentUserId());
        }

        public static async Task DeleteComment(Guid commentId)
        {
            // Throw an exception if the user is not logged in.
            if (GameStateManager.GetCurrentUser() == null) throw new Exception("User must be logged in!");

            await CommentRepository.DeleteCommentAsync(commentId);
        }


        #endregion

        #region Leaderboard
        public static async Task<List<User>> GetAllUsersSortedByCoinsAsync()
        {
            // Get a list with all the users from the database.
            List<User> users = await UserRepository.GetAllUsersAsync();

            // Sort the users by coins.
            users.Sort((user1, user2) => user2.Coins.CompareTo(user1.Coins));

            return users;
        }

        #endregion
    }
}
