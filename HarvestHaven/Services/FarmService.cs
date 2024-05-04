using HarvestHaven.Entities;
using HarvestHaven.Repositories;
using HarvestHaven.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarvestHaven.Services
{
    public static class FarmService
    {
        public static async Task<Dictionary<FarmCell, Item>> GetAllFarmCellsForUser(Guid userId)
        {
            // Get all the user's farm cells.
            List<FarmCell> farmCells = await FarmCellRepository.GetUserFarmCellsAsync(userId);

            // Initialize the dictionary that will be returned.
            Dictionary<FarmCell, Item> farmCellsMap = new Dictionary<FarmCell, Item>();

            // Go thorugh all the farm cells.
            foreach (FarmCell cell in farmCells)
            {
                // IMPORTANT: Remove the cell from the database if it has not been interacted with for longer than a cell's lifetime.
                if (DateTime.UtcNow - cell.LastTimeInteracted >= TimeSpan.FromDays(Constants.FARM_CELL_LIFETIME_IN_DAYS))
                { 
                    await FarmCellRepository.DeleteFarmCellAsync(cell.Id);
                    continue; 
                }             

                // Get the item from the current cell.
                Item item = await ItemRepository.GetItemByIdAsync(cell.ItemId);
                if (item == null) throw new Exception($"Item from farmCell with id {cell.Id} not found.");

                // Add the item - cell pair in the dictionary.
                farmCellsMap.Add(cell, item);
            }

            return farmCellsMap;
        }

        public static async Task InteractWithCell(int row, int column)
        {
            #region Validation
            // Throw an exception if the user is not logged in.
            if (GameStateManager.GetCurrentUser() == null) throw new Exception("User must be logged in!");

            // Get the farm cell from the given position.
            FarmCell farmCell = await FarmCellRepository.GetUserFarmCellByPositionAsync(GameStateManager.GetCurrentUserId(), row, column);
            if (farmCell == null) throw new Exception("No farm cell found at the given position for the current user in the database!");

            // Get the item from the farm cell.
            Item farmCellItem = await ItemRepository.GetItemByIdAsync(farmCell.ItemId);
            if (farmCellItem == null) throw new Exception("Item from the farm cell was not found in the database!");

            // Get the required resource of the farm cell item from the inventory.
            InventoryResource requiredResource = await InventoryResourceRepository.GetUserResourceByResourceIdAsync(GameStateManager.GetCurrentUserId(), farmCellItem.RequiredResourceId);
            if (requiredResource == null || requiredResource.Quantity <= 0) throw new Exception("You don't have the resource required for this farm cell.");
            #endregion

            // Update the user's inventory by decreasing the required resource from the farm cell item.
            requiredResource.Quantity--;
            await InventoryResourceRepository.UpdateUserResourceAsync(requiredResource);

            // Get the user's interact farm cell item resource from the inventory.
            InventoryResource interactResource = await InventoryResourceRepository.GetUserResourceByResourceIdAsync(GameStateManager.GetCurrentUserId(), farmCellItem.InteractResourceId);

            // Define the interact resource quantity amount.
            int interactResourceAmount = (DateTime.UtcNow - farmCell.LastTimeEnhanced < TimeSpan.FromDays(Constants.ENCHANCE_DURATION_IN_DAYS)) ? 2 : 1;

            // If the user already has this resource in his inventory simply update the quantity.
            if (interactResource != null)
            {
                interactResource.Quantity += interactResourceAmount;
                await InventoryResourceRepository.UpdateUserResourceAsync(interactResource);
            }
            else // Otherwise create the entry in the database.
            {
                await InventoryResourceRepository.AddUserResourceAsync(new InventoryResource(
                    id: Guid.NewGuid(),
                    userId: GameStateManager.GetCurrentUserId(),
                    resourceId: farmCellItem.InteractResourceId,
                    quantity: interactResourceAmount
                    ));
            }

            // Update the farm cell's last interaction time in the database.
            farmCell.LastTimeInteracted = DateTime.UtcNow;
            await FarmCellRepository.UpdateFarmCellAsync(farmCell);

            // Check achievements.
            await AchievementService.CheckInventoryAchievements();
        }

        public static async Task DestroyCell(int row, int column)
        {
            #region Validation
            // Throw an exception if the user is not logged in.
            if (GameStateManager.GetCurrentUser() == null) throw new Exception("User must be logged in!");
            
            // Get the farm cell from the given position.
            FarmCell farmCell = await FarmCellRepository.GetUserFarmCellByPositionAsync(GameStateManager.GetCurrentUserId(), row, column);
            if (farmCell == null) throw new Exception("No farm cell found at the given position for the current user in the database!");

            // Get the item from the farm cell.
            Item farmCellItem = await ItemRepository.GetItemByIdAsync(farmCell.ItemId);
            if (farmCellItem == null) throw new Exception("Item from the farm cell was not found in the database!");
            #endregion

            // If the farm cell item has a destroy resource.
            if(farmCellItem.DestroyResourceId != null)
            {
                // Get the user's destroy farm cell item resource from the inventory.
                InventoryResource destroyResource = await InventoryResourceRepository.GetUserResourceByResourceIdAsync(GameStateManager.GetCurrentUserId(), farmCellItem.DestroyResourceId.Value);

                // Define the destroy resource quantity amount.
                int destroyResourceAmount = (DateTime.UtcNow - farmCell.LastTimeEnhanced < TimeSpan.FromDays(Constants.ENCHANCE_DURATION_IN_DAYS)) ? 2 : 1;

                // If the user already has this resource in his inventory simply update the quantity.
                if (destroyResource != null)
                {
                    destroyResource.Quantity += destroyResourceAmount;
                    await InventoryResourceRepository.UpdateUserResourceAsync(destroyResource);
                }
                else // Otherwise create the entry in the database.
                {
                    await InventoryResourceRepository.AddUserResourceAsync(new InventoryResource(
                        id: Guid.NewGuid(),
                        userId: GameStateManager.GetCurrentUserId(),
                        resourceId: farmCellItem.DestroyResourceId.Value,
                        quantity: destroyResourceAmount
                        ));
                }
            }

            // Remove the cell from the database.
            await FarmCellRepository.DeleteFarmCellAsync(farmCell.Id);

            // Check achievements.
            await AchievementService.CheckInventoryAchievements();
        }

        public static async Task EnchanceCellForUser(Guid targetUserId, int row, int column)
        {
            #region Validation
            // Throw an exception if the user is not logged in.
            if (GameStateManager.GetCurrentUser() == null) throw new Exception("User must be logged in!");

            // Throw an exception if the user wants to enhance his own cell.
            if (GameStateManager.GetCurrentUserId() == targetUserId) throw new Exception("You cannot enhance you own cell!");

            // Get the farm cell from the given position.
            FarmCell farmCell = await FarmCellRepository.GetUserFarmCellByPositionAsync(targetUserId, row, column);
            if (farmCell == null) throw new Exception("No farm cell found at the given position for the target user in the database!");

            // Throw an exception if the cell is already enhanced.        
            if (DateTime.UtcNow - farmCell.LastTimeEnhanced < TimeSpan.FromDays(Constants.ENCHANCE_DURATION_IN_DAYS)) throw new Exception("Cell is already enhanced!");
            #endregion

            // Enhance the cell.
            farmCell.LastTimeEnhanced = DateTime.UtcNow;
            await FarmCellRepository.UpdateFarmCellAsync(farmCell);
        }

        public static async Task<bool> IsCellEnchanced(Guid userId, int row, int column)
        {
            #region Validation
            // Throw an exception if the user is not logged in.
            if (GameStateManager.GetCurrentUser() == null) throw new Exception("User must be logged in!");

            // Get the farm cell from the given position.
            FarmCell farmCell = await FarmCellRepository.GetUserFarmCellByPositionAsync(userId, row, column);
            if (farmCell == null) throw new Exception("No farm cell found at the given position for the target user in the database!");
            #endregion

            // Return whether the cell is enhanced or not.
            return (DateTime.UtcNow - farmCell.LastTimeEnhanced < TimeSpan.FromDays(Constants.ENCHANCE_DURATION_IN_DAYS));
        }
    }
}
