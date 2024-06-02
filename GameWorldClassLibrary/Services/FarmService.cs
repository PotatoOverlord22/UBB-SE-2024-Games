using GameWorldClassLibrary.Models;
using GameWorldClassLibrary.Repositories;
using GameWorldClassLibrary.Utils;
using GameWorldClassLibrary.Services.Interfaces;

namespace GameWorldClassLibrary.Services
{
    public class FarmService : IFarmService
    {
        private readonly IAchievementService achievementService;
        private readonly IFarmCellRepository farmCellRepository;
        private readonly IItemRepository itemRepository;
        private readonly IInventoryResourceRepository inventoryResourceRepository;
        public FarmService(IAchievementService achievementService, IFarmCellRepository farmCellRepository, IItemRepository itemRepository, IInventoryResourceRepository inventoryResourceRepository)
        {
            this.achievementService = achievementService;
            this.farmCellRepository = farmCellRepository;
            this.itemRepository = itemRepository;
            this.inventoryResourceRepository = inventoryResourceRepository;
        }
        public async Task<Dictionary<FarmCell, Item>> GetAllFarmCellsForUser(Guid userId)
        {
            // Get all the user's farm cells.
            List<FarmCell> farmCells = await farmCellRepository.GetUserFarmCellsAsync(userId);

            // Initialize the dictionary that will be returned.
            Dictionary<FarmCell, Item> farmCellsMap = new Dictionary<FarmCell, Item>();

            // Go thorugh all the farm cells.
            foreach (FarmCell cell in farmCells)
            {
                // IMPORTANT: Remove the cell from the database if it has not been interacted with for longer than a cell's lifetime.
                if (DateTime.UtcNow - cell.LastTimeInteracted >= TimeSpan.FromDays(Constants.FARM_CELL_LIFETIME_IN_DAYS))
                {
                    await farmCellRepository.DeleteFarmCellAsync(cell.Id);
                    continue;
                }

                // Get the item from the current cell.
                Item item = await itemRepository.GetItemByIdAsync(cell.Item.Id);
                if (item == null)
                {
                    throw new Exception($"Item from farmCell with id {cell.Id} not found.");
                }

                // Add the item - cell pair in the dictionary.
                farmCellsMap.Add(cell, item);
            }

            return farmCellsMap;
        }

        public async Task InteractWithCell(int row, int column)
        {
            // Throw an exception if the user is not logged in.
            if (GameStateManager.GetCurrentUser() == null)
            {
                throw new Exception("User must be logged in!");
            }

            // Get the farm cell from the given position.
            FarmCell farmCell = await farmCellRepository.GetUserFarmCellByPositionAsync(GameStateManager.GetCurrentUserId(), row, column);
            if (farmCell == null)
            {
                throw new Exception("No farm cell found at the given position for the current user in the database!");
            }

            // Get the item from the farm cell.
            Item farmCellItem = await itemRepository.GetItemByIdAsync(farmCell.Item.Id);
            if (farmCellItem == null)
            {
                throw new Exception("Item from the farm cell was not found in the database!");
            }

            // Get the required resource of the farm cell item from the inventory.
            InventoryResource requiredResource = await inventoryResourceRepository.GetUserResourceByResourceIdAsync(GameStateManager.GetCurrentUserId(), farmCellItem.ResourceToPlace.Id);
            if (requiredResource == null || requiredResource.Quantity <= 0)
            {
                throw new Exception("You don't have the resource required for this farm cell.");
            }

            // Update the user's inventory by decreasing the required resource from the farm cell item.
            requiredResource.Quantity--;
            await inventoryResourceRepository.UpdateUserResourceAsync(requiredResource);

            // Get the user's interact farm cell item resource from the inventory.
            InventoryResource interactResource = await inventoryResourceRepository.GetUserResourceByResourceIdAsync(GameStateManager.GetCurrentUserId(), farmCellItem.ResourceToInteract.Id);

            // Define the interact resource quantity amount.
            int interactResourceAmount = DateTime.UtcNow - farmCell.LastTimeEnhanced < TimeSpan.FromDays(Constants.ENCHANCE_DURATION_IN_DAYS) ? 2 : 1;

            // If the user already has this resource in his inventory simply update the quantity.
            if (interactResource != null)
            {
                interactResource.Quantity += interactResourceAmount;
                await inventoryResourceRepository.UpdateUserResourceAsync(interactResource);
            }
            else
            {
                // Otherwise create the entry in the database.
                await inventoryResourceRepository.AddUserResourceAsync(new InventoryResource(
                    id: Guid.NewGuid(),
                    owner: null,
                    resource: farmCellItem.ResourceToInteract,
                    quantity: interactResourceAmount));
            }

            // Update the farm cell's last interaction time in the database.
            farmCell.LastTimeInteracted = DateTime.UtcNow;
            await farmCellRepository.UpdateFarmCellAsync(farmCell);

            // Check achievements.
            await achievementService.CheckInventoryAchievements();
        }

        public async Task DestroyCell(int row, int column)
        {
            // Throw an exception if the user is not logged in.
            if (GameStateManager.GetCurrentUser() == null)
            {
                throw new Exception("User must be logged in!");
            }

            // Get the farm cell from the given position.
            FarmCell farmCell = await farmCellRepository.GetUserFarmCellByPositionAsync(GameStateManager.GetCurrentUserId(), row, column);
            if (farmCell == null)
            {
                throw new Exception("No farm cell found at the given position for the current user in the database!");
            }

            // Get the item from the farm cell.
            Item farmCellItem = await itemRepository.GetItemByIdAsync(farmCell.Item.Id);
            if (farmCellItem == null)
            {
                throw new Exception("Item from the farm cell was not found in the database!");
            }

            // If the farm cell item has a destroy resource.
            if (farmCellItem.ResourceToDestroy.Id != null)
            {
                // Get the user's destroy farm cell item resource from the inventory.
                InventoryResource destroyResource = await inventoryResourceRepository.GetUserResourceByResourceIdAsync(GameStateManager.GetCurrentUserId(), farmCellItem.ResourceToDestroy.Id);

                // Define the destroy resource quantity amount.
                int destroyResourceAmount = DateTime.UtcNow - farmCell.LastTimeEnhanced < TimeSpan.FromDays(Constants.ENCHANCE_DURATION_IN_DAYS) ? 2 : 1;

                // If the user already has this resource in his inventory simply update the quantity.
                if (destroyResource != null)
                {
                    destroyResource.Quantity += destroyResourceAmount;
                    await inventoryResourceRepository.UpdateUserResourceAsync(destroyResource);
                }
                else
                {
                    // Otherwise create the entry in the database.
                    await inventoryResourceRepository.AddUserResourceAsync(new InventoryResource(
                        id: Guid.NewGuid(),
                        owner: null,
                        resource: farmCellItem.ResourceToDestroy,
                        quantity: destroyResourceAmount));
                }
            }

            // Remove the cell from the database.
            await farmCellRepository.DeleteFarmCellAsync(farmCell.Id);

            // Check achievements.
            await achievementService.CheckInventoryAchievements();
        }

        public async Task EnchanceCellForUser(Guid targetUserId, int row, int column)
        {
            // Throw an exception if the user is not logged in.
            if (GameStateManager.GetCurrentUser() == null)
            {
                throw new Exception("User must be logged in!");
            }

            // Throw an exception if the user wants to enhance his own cell.
            if (GameStateManager.GetCurrentUserId() == targetUserId)
            {
                throw new Exception("You cannot enhance you own cell!");
            }

            // Get the farm cell from the given position.
            FarmCell farmCell = await farmCellRepository.GetUserFarmCellByPositionAsync(targetUserId, row, column);
            if (farmCell == null)
            {
                throw new Exception("No farm cell found at the given position for the target user in the database!");
            }

            // Throw an exception if the cell is already enhanced.
            if (DateTime.UtcNow - farmCell.LastTimeEnhanced < TimeSpan.FromDays(Constants.ENCHANCE_DURATION_IN_DAYS))
            {
                throw new Exception("Cell is already enhanced!");
            }

            // Enhance the cell.
            farmCell.LastTimeEnhanced = DateTime.UtcNow;
            await farmCellRepository.UpdateFarmCellAsync(farmCell);
        }

        public async Task<bool> IsCellEnchanced(Guid userId, int row, int column)
        {
            // Throw an exception if the user is not logged in.
            if (GameStateManager.GetCurrentUser() == null)
            {
                throw new Exception("User must be logged in!");
            }

            // Get the farm cell from the given position.
            FarmCell farmCell = await farmCellRepository.GetUserFarmCellByPositionAsync(userId, row, column);
            if (farmCell == null)
            {
                throw new Exception("No farm cell found at the given position for the target user in the database!");
            }

            // Return whether the cell is enhanced or not.
            return DateTime.UtcNow - farmCell.LastTimeEnhanced < TimeSpan.FromDays(Constants.ENCHANCE_DURATION_IN_DAYS);
        }

        public string GetPicturePathByItemType(ItemType type)
        {
            return type switch
            {
                ItemType.CarrotSeeds => Constants.CarrotPath,
                ItemType.CornSeeds => Constants.CornPath,
                ItemType.WheatSeeds => Constants.WheatPath,
                ItemType.TomatoSeeds => Constants.TomatoPath,
                ItemType.Chicken => Constants.ChickenPath,
                ItemType.Duck => Constants.DuckPath,
                ItemType.Sheep => Constants.SheepPath,
                ItemType.Cow => Constants.CowPath,
                _ => throw new Exception("Invalid item type!")
            };
        }
    }
}
