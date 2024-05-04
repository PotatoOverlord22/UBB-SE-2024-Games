using HarvestHaven.Entities;
using HarvestHaven.Repositories;
using HarvestHaven.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace HarvestHaven.Services
{
    public static class MarketService
    {
        public static async Task BuyItem(int row, int column, ItemType itemType)
        {
            #region Validation
            // Throw an exception if the user is not logged in.
            if (GameStateManager.GetCurrentUser() == null) throw new Exception("User must be logged in!");
            
            // Get the item from the database.
            Item item = await ItemRepository.GetItemByTypeAsync(itemType);
            if (item == null) throw new Exception("Given item not found in the database.");

            // Get the corresponding market buy item from the database.
            MarketBuyItem marketBuyItem = await MarketBuyItemRepository.GetMarketBuyItemByItemIdAsync(item.Id);
            if (marketBuyItem == null) throw new Exception("Market buy item not found in the database.");

            // Throw an exception if the user doesn't have enough money to buy the item.
            if (GameStateManager.GetCurrentUser().Coins < marketBuyItem.BuyPrice) throw new Exception("Not enough money!");

            // Get all the user farm cells from the database.
            List<FarmCell> farmCells = await FarmCellRepository.GetUserFarmCellsAsync(GameStateManager.GetCurrentUserId());

            // Throw an exception in case the cell is occupied.
            foreach(FarmCell cell in farmCells)           
                if(cell.Row == row && cell.Column == column) 
                    throw new Exception("Cell is occupied.");

            #endregion

            // Add a new farm cell in the database.
            await FarmCellRepository.AddFarmCellAsync(new FarmCell(
                id: Guid.NewGuid(),
                userId: GameStateManager.GetCurrentUserId(),
                row: row,
                column: column,
                itemId: item.Id,
                lastTimeEnhanced: null,
                lastTimeInteracted: null
                ));

            // Update the user coins and number of items bought both locally and in the database.
            User newUser = GameStateManager.GetCurrentUser();
            newUser.Coins -= marketBuyItem.BuyPrice;
            newUser.NrItemsBought++;
            await UserRepository.UpdateUserAsync(newUser);
            GameStateManager.SetCurrentUser(newUser);

            // Check achievements.
            await AchievementService.CheckFarmAchievements();
            await AchievementService.CheckMarketAchievements();
        }

        public static async Task SellResource(ResourceType resourceType)
        {
            #region Validation
            // Throw an exception if the user is not logged in.
            if (GameStateManager.GetCurrentUser() == null) throw new Exception("User must be logged in!");

            // Get the resource from the database.
            Resource resource = await ResourceRepository.GetResourceByTypeAsync(resourceType);
            if (resource == null) throw new Exception("Given resource not found.");

            // Get the market sell resource from the database.
            MarketSellResource marketSellResouce = await MarketSellResourceRepository.GetMarketSellResourceByResourceIdAsync(resource.Id);
            if (marketSellResouce == null) throw new Exception("Market sell resource not found.");

            // Get the user's inventory resource from the database.
            InventoryResource? inventoryResource = await InventoryResourceRepository.GetUserResourceByResourceIdAsync(GameStateManager.GetCurrentUserId(), resource.Id);

            // Throw an exception if the user doesn's have that resource.
            if (inventoryResource == null || inventoryResource.Quantity <= 0) throw new Exception("You do not own any " + resource.ResourceType.ToString() + "!");
            #endregion

            // Update the inventory resource quantity in the database.
            inventoryResource.Quantity--;
            await InventoryResourceRepository.UpdateUserResourceAsync(inventoryResource);

            // Update the user coins both locally and in the database.
            User newUser = GameStateManager.GetCurrentUser();
            newUser.Coins += marketSellResouce.SellPrice;
            await UserRepository.UpdateUserAsync(newUser);
            GameStateManager.SetCurrentUser(newUser);

            // Check achievements.
            await AchievementService.CheckInventoryAchievements();
        }
    }
}
