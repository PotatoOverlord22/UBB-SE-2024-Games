using HarvestHaven.Entities;
using HarvestHaven.Repositories;
using HarvestHaven.Utils;

namespace HarvestHaven.Services
{
    public static class TradeService
    {
        public static async Task<List<Trade>> GetAllTradesExceptCreatedByLoggedUser()
        {
            // Throw an exception if the user is not logged in.
            if (GameStateManager.GetCurrentUser() == null) throw new Exception("User must be logged in!");

            return await TradeRepository.GetAllTradesExceptCreatedByUser(GameStateManager.GetCurrentUserId());
        }

        public static async Task<Trade> GetUserTradeAsync(Guid userId)
        {
            return await TradeRepository.GetUserTradeAsync(userId);
        }

        public static async Task CreateTradeAsync(ResourceType givenResourceType, int givenResourceQuantity, ResourceType requestedResourceType, int requestedResourceQuantity)
        {
            #region Validation
            // Throw an exception if the user is not logged in.
            if (GameStateManager.GetCurrentUser() == null) throw new Exception("User must be logged in!");

            // Get the given resource from the database.
            Resource? givenResource = await ResourceRepository.GetResourceByTypeAsync(givenResourceType);
            if (givenResource == null) throw new Exception("Given resource was not found in the database!");

            // Get the requested resource from the database.
            Resource? requestedResource = await ResourceRepository.GetResourceByTypeAsync(requestedResourceType);
            if (requestedResource == null) throw new Exception("Requested resource was not found in the database!");

            // Get the user's given trade resource from the inventory and throw and error in case the user doesn't have enough quantity.
            InventoryResource userGivenResource = await InventoryResourceRepository.GetUserResourceByResourceIdAsync(GameStateManager.GetCurrentUserId(), givenResource.Id);
            if (userGivenResource == null || userGivenResource.Quantity < givenResourceQuantity) throw new Exception($"You don't have that ammount of {givenResourceType.ToString()}!");
            #endregion

            // Update the user's resource quantity in the database.
            userGivenResource.Quantity -= givenResourceQuantity;
            await InventoryResourceRepository.UpdateUserResourceAsync(userGivenResource);

            // Create the trade in the database.
            await TradeRepository.CreateTradeAsync(new Trade(
                id: Guid.NewGuid(),
                userId: GameStateManager.GetCurrentUserId(),
                givenResourceId: givenResource.Id,
                givenResourceQuantity: givenResourceQuantity,
                requestedResourceId: requestedResource.Id,
                requestedResourceQuantity: requestedResourceQuantity,
                createdTime: DateTime.UtcNow,
                isCompleted: false             
                ));        
        }

        public static async Task PerformTradeAsync(Guid tradeId)
        {
            #region Validation
            // Throw an exception if the user is not logged in.
            if (GameStateManager.GetCurrentUser() == null) throw new Exception("User must be logged in!");

            // Get the trade from the database.
            Trade trade = await TradeRepository.GetTradeByIdAsync(tradeId);
            if (trade == null) throw new Exception("Trade not found in the database!");

            // Get the trade's requested resource from the database.
            Resource requestedResource = await ResourceRepository.GetResourceByIdAsync(trade.RequestedResourceId);
            if (requestedResource == null) throw new Exception("Requested resource not found in the database!");

            // Get the user's requested trade resource from the inventory and Throw an exception in case the user doesn't have enough quantity.
            InventoryResource userRequestedResource = await InventoryResourceRepository.GetUserResourceByResourceIdAsync(GameStateManager.GetCurrentUserId(), requestedResource.Id);
            if (userRequestedResource == null || userRequestedResource.Quantity < trade.RequestedResourceQuantity) throw new Exception($"You don't have that ammount of {requestedResource.ResourceType.ToString()}!");
            #endregion

            // Update the user's inventory resources by removing the requested trade resource quantity.
            userRequestedResource.Quantity -= trade.RequestedResourceQuantity;
            await InventoryResourceRepository.UpdateUserResourceAsync(userRequestedResource);      

            // Get the user's given trade resource from the inventory.
            InventoryResource userGivenResource = await InventoryResourceRepository.GetUserResourceByResourceIdAsync(GameStateManager.GetCurrentUserId(), trade.GivenResourceId);
            // If the user already has this resource in his inventory simply update the quantity.
            if (userGivenResource != null)
            {
                userGivenResource.Quantity += trade.GivenResourceQuantity;
                await InventoryResourceRepository.UpdateUserResourceAsync(userGivenResource);
            }
            else // Otherwise create the entry in the database.
            {
                await InventoryResourceRepository.AddUserResourceAsync(new InventoryResource(
                    id: Guid.NewGuid(),
                    userId: GameStateManager.GetCurrentUserId(),
                    resourceId: trade.GivenResourceId,
                    quantity: trade.GivenResourceQuantity
                    ));
            }

            // FOR THE OTHER USER INVOLVED. Get the user's requested trade resource from the inventory of the user who iniated the trade.
            InventoryResource initialRequestedResource = await InventoryResourceRepository.GetUserResourceByResourceIdAsync(trade.UserId, trade.RequestedResourceId);

            // If the user already has this resource in his inventory simply update the quantity.
            if (initialRequestedResource != null)
            {
                initialRequestedResource.Quantity += trade.RequestedResourceQuantity;
                await InventoryResourceRepository.UpdateUserResourceAsync(initialRequestedResource);
            }
            else // Otherwise create the entry in the database.
            {
                await InventoryResourceRepository.AddUserResourceAsync(new InventoryResource(
                    id: Guid.NewGuid(),
                    userId: trade.UserId,
                    resourceId: trade.RequestedResourceId,
                    quantity: trade.RequestedResourceQuantity
                    ));
            }

            // Remove the trade from the database.
            await TradeRepository.DeleteTradeAsync(trade.Id);

            // Increase the other user's number of trades.
            User otherUser = await UserRepository.GetUserByIdAsync(trade.UserId);
            if (otherUser == null) throw new Exception("The user that created the trade with cannot be found in the database.");
            otherUser.NrTradesPerformed++;
            await UserRepository.UpdateUserAsync(otherUser);

            // Update the user's number of trades in the database and locally.
            User newUser = GameStateManager.GetCurrentUser();
            newUser.NrTradesPerformed++;
            await UserRepository.UpdateUserAsync(newUser);
            GameStateManager.SetCurrentUser(newUser);

            // Check achievements.
            await AchievementService.CheckTradeAchievements(trade.UserId);
            await AchievementService.CheckInventoryAchievements();
        }

        public static async Task CancelTradeAsync(Guid tradeId)
        {
            #region Validation
            // Throw an exception if the user is not logged in.
            if (GameStateManager.GetCurrentUser() == null) throw new Exception("User must be logged in!");

            // Get the trade from the database.
            Trade trade = await TradeRepository.GetTradeByIdAsync(tradeId);
            if (trade == null) throw new Exception("Trade not found in the database!");
            #endregion

            // Get the user's given trade resource from the inventory.
            InventoryResource userGivenResource = await InventoryResourceRepository.GetUserResourceByResourceIdAsync(GameStateManager.GetCurrentUserId(), trade.GivenResourceId);

            // If the user already has this resource in his inventory simply update the quantity.
            if (userGivenResource != null)
            {
                userGivenResource.Quantity += trade.GivenResourceQuantity;
                await InventoryResourceRepository.UpdateUserResourceAsync(userGivenResource);
            }
            else // Otherwise create the entry in the database.
            {
                await InventoryResourceRepository.AddUserResourceAsync(new InventoryResource(
                    id: Guid.NewGuid(),
                    userId: GameStateManager.GetCurrentUserId(),
                    resourceId: trade.GivenResourceId,
                    quantity: trade.GivenResourceQuantity
                    ));
            }

            // Remove the trade from the database.
            await TradeRepository.DeleteTradeAsync(trade.Id);

            // Check achievements.
            await AchievementService.CheckInventoryAchievements();
        }
    }
}
