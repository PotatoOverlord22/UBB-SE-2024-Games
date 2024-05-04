using HarvestHaven.Repositories;
using HarvestHaven.Entities;
using System.Diagnostics;
using HarvestHaven.Utils;
using System.Windows;
using System.Data.Common;

namespace HarvestHaven.Services
{
    public static class AchievementService
    {
        
        public static async Task<List<Achievement>> GetAllAchievementsAsync()
        {
            // Throw an exception if the user is not logged in.
            if (GameStateManager.GetCurrentUser() == null) throw new Exception("User must be logged in!");

            return await AchievementRepository.GetAllAchievementsAsync();
        }

        public static async Task<Dictionary<UserAchievement, Achievement>> GetUserAchievements()
        {
            // Throw an exception if the user is not logged in.
            if (GameStateManager.GetCurrentUser() == null) throw new Exception("User must be logged in!");

            // Get all user achievements from the database.
            List<UserAchievement> userAchievements = await UserAchievementRepository.GetAllUserAchievementsAsync(GameStateManager.GetCurrentUserId());

            // Initialize the dictionary that will be returned.
            Dictionary<UserAchievement, Achievement> userAchievementsMap = new Dictionary<UserAchievement, Achievement>();

            // Go through each user achievement.
            foreach (UserAchievement userAchievement in userAchievements)
            {
                // Get the corresponding achievement from the database.
                Achievement achievement = await AchievementRepository.GetAchievementByIdAsync(userAchievement.AchievementId);
                if (achievement == null) throw new Exception($"No corresponding achievement found for the user achievement with id: {userAchievement.Id}");

                // Add the pair in the dictionary.
                userAchievementsMap.Add(userAchievement, achievement);
            }

            return userAchievementsMap;
        }

        public static async Task CheckFarmAchievements()
        {
            // Throw an exception if the user is not logged in.
            if (GameStateManager.GetCurrentUser() == null) throw new Exception("User must be logged in!");

            // Get all the user farm cells.
            List<FarmCell> farmCells = await FarmCellRepository.GetUserFarmCellsAsync(GameStateManager.GetCurrentUserId());

            #region Put 5 cows on grids so it makes an X shape! d532eeca-7163-4b27-8135-dbca4cee057a

            async Task<bool> IsCowAtPosition(int row, int column, List<FarmCell> farmCells)
            {
                // Go throguh all the farm cells.
                foreach(FarmCell cell in farmCells)
                {
                    // Skip the ones that do not match the given row and the column.
                    if(cell.Row != row || cell.Column != column) continue;

                    // Get the item from the cell.
                    Item cellItem = await ItemRepository.GetItemByIdAsync(cell.ItemId);

                    return cellItem?.ItemType == ItemType.Cow;
                }

                // Return false if the cell is not found.
                return false;
            }

            // Go through all the farm cells.
            foreach (FarmCell cell in farmCells)
            {
                // Get the item from the cell and skip null items or non-cow items.
                Item item = await ItemRepository.GetItemByIdAsync(cell.ItemId);
                if (item == null || item.ItemType != ItemType.Cow) continue;

                // Check if the current cell is a potential center of the X shape.
                if (await IsCowAtPosition(cell.Row - 1, cell.Column - 1, farmCells) &&
                    await IsCowAtPosition(cell.Row - 1, cell.Column + 1, farmCells) &&
                    await IsCowAtPosition(cell.Row + 1, cell.Column - 1, farmCells) &&
                    await IsCowAtPosition(cell.Row + 1, cell.Column + 1, farmCells))
                {
                    await AddUserAchievement(Guid.Parse("d532eeca-7163-4b27-8135-dbca4cee057a"));
                    break;
                }
            }
            #endregion

            #region Have the same number of cows, sheep, duck, chicken on your farm! a3b0c4bb-2f1b-4189-9be0-0cc25bf65868

            // Initialize the counters.
            int cows = 0, sheeps = 0, ducks = 0, chickens = 0;

            foreach (FarmCell cell in farmCells)
            {
                // Get the item from the cell and skip null items.
                Item item = await ItemRepository.GetItemByIdAsync(cell.ItemId);
                if (item == null) continue;

                switch (item.ItemType)
                {
                    case ItemType.Cow: cows++; break;
                    case ItemType.Sheep: sheeps++; break;
                    case ItemType.Duck: ducks++; break;
                    case ItemType.Chicken: chickens++; break;
                }
            }

            if (cows == sheeps && sheeps == ducks && ducks == chickens && cows > 0) await AddUserAchievement(Guid.Parse("a3b0c4bb-2f1b-4189-9be0-0cc25bf65868"));

            #endregion

            #region Put carrot/wheat/tomato/sheep/chicken/cow in each corner of the land! 37b6e693-c25b-4a1d-ae9e-904148be486c

            // Define the target item types.
            ItemType[] targetItemTypes = { ItemType.CarrotSeeds, ItemType.WheatSeeds, ItemType.TomatoSeeds, ItemType.Sheep, ItemType.Chicken, ItemType.Cow };

            // Create a structure to mark row and column positions.
            bool[,] markedPositions = new bool[Constants.FARM_SIZE + 1, Constants.FARM_SIZE + 1];

            // Go through all the farm cells.
            foreach (FarmCell cell in farmCells)
            {
                // Get the item from the cell and skip null items..
                Item item = await ItemRepository.GetItemByIdAsync(cell.ItemId);
                if (item != null && targetItemTypes.Contains(item.ItemType))
                {
                    // Mark the row and column positions for cells containing one of the target item types.
                    markedPositions[cell.Row, cell.Column] = true;
                }
            }

            // Check if the corners are marked.
            bool allCornersMarked = markedPositions[1, 1] && markedPositions[1, Constants.FARM_SIZE] && markedPositions[Constants.FARM_SIZE, 1] && markedPositions[Constants.FARM_SIZE, Constants.FARM_SIZE];
            if (allCornersMarked) await AddUserAchievement(Guid.Parse("37b6e693-c25b-4a1d-ae9e-904148be486c"));

            #endregion
        }

        public static async Task CheckTradeAchievements(Guid otherUserInvolvedId)
        {
            // Throw an exception if the user is not logged in.
            if (GameStateManager.GetCurrentUser() == null) throw new Exception("User must be logged in!");

            #region Trade with a player! 15a0ffbb-3e0a-4a5e-8a87-4bf35395622e
            if (GameStateManager.GetCurrentUser().NrTradesPerformed >= 1) await AddUserAchievement(Guid.Parse("15a0ffbb-3e0a-4a5e-8a87-4bf35395622e"));
            #endregion

            #region Trade with 3 players! 8f01cc9d-e620-4719-96af-e3c078a85b4d
            if (GameStateManager.GetCurrentUser().NrTradesPerformed >= 3) await AddUserAchievement(Guid.Parse("8f01cc9d-e620-4719-96af-e3c078a85b4d"));
            #endregion

            #region Trade with 5 players! bf6f36e4-718a-4b9e-a991-e02d64257cbf
            if (GameStateManager.GetCurrentUser().NrTradesPerformed >= 5) await AddUserAchievement(Guid.Parse("bf6f36e4-718a-4b9e-a991-e02d64257cbf"));
            #endregion

            #region Make the very first trade provided by a player! a8f407e3-5338-4bf8-bd20-61bf6fa78aa9
            User otherUser = await UserRepository.GetUserByIdAsync(otherUserInvolvedId);
            if(otherUser != null && otherUser.NrTradesPerformed == 1) await AddUserAchievement(Guid.Parse("a8f407e3-5338-4bf8-bd20-61bf6fa78aa9"));
            #endregion
        }

        public static async Task CheckInventoryAchievements()
        {
            // Throw an exception if the user is not logged in.
            if (GameStateManager.GetCurrentUser() == null) throw new Exception("User must be logged in!");

            // Get the neccesary resources from the current user's inventory.
            InventoryResource wheatInventoryResource = await UserService.GetInventoryResourceByType(ResourceType.Wheat);
            InventoryResource tomatoInventoryResource = await UserService.GetInventoryResourceByType(ResourceType.Tomato);
            InventoryResource cornInventoryResource = await UserService.GetInventoryResourceByType(ResourceType.Corn);
            InventoryResource chickenEggsInventoryResource = await UserService.GetInventoryResourceByType(ResourceType.ChickenEgg);
            InventoryResource duckEggsInventoryResource = await UserService.GetInventoryResourceByType(ResourceType.DuckEgg);
            InventoryResource chickenMeatInventoryResource = await UserService.GetInventoryResourceByType(ResourceType.ChickenMeat);
            InventoryResource duckMeatInventoryResource = await UserService.GetInventoryResourceByType(ResourceType.DuckMeat);

            #region Have in your inventory exactly 69 wheat! bccadd9c-c520-4c6e-8efb-8ef7642edde0
            if (wheatInventoryResource != null && wheatInventoryResource.Quantity == 69) await AddUserAchievement(Guid.Parse("bccadd9c-c520-4c6e-8efb-8ef7642edde0"));
            #endregion

            #region Collect exactly 25 eggs! be3aba07-1741-4194-b282-103919dcca0f         
            int? sum = chickenEggsInventoryResource?.Quantity + duckEggsInventoryResource?.Quantity;
            if(sum != null && sum == 25) await AddUserAchievement(Guid.Parse("be3aba07-1741-4194-b282-103919dcca0f"));
            #endregion

            #region Have the same amount of chicken and duck meat! 902d847c-c056-4a77-8573-581c22521f7a
            if (chickenMeatInventoryResource != null && duckMeatInventoryResource != null && chickenMeatInventoryResource.Quantity == duckMeatInventoryResource.Quantity) await AddUserAchievement(Guid.Parse("902d847c-c056-4a77-8573-581c22521f7a"));
            #endregion

            #region Have the same amount of chicken and duck eggs! 1bbe52db-494a-4af1-9997-37d4202b7165
            if (chickenEggsInventoryResource != null && duckEggsInventoryResource != null && chickenEggsInventoryResource.Quantity == duckEggsInventoryResource.Quantity) await AddUserAchievement(Guid.Parse("1bbe52db-494a-4af1-9997-37d4202b7165"));
            #endregion                           

            #region Sell all of your tomatoes/chicken eggs or duck eggs/corn! e6149f4d-3f2f-485d-ad7b-e4c5f5660f35
            if ((tomatoInventoryResource?.Quantity == 0 && chickenEggsInventoryResource?.Quantity == 0) || (duckEggsInventoryResource?.Quantity == 0 && cornInventoryResource?.Quantity == 0)) await AddUserAchievement(Guid.Parse("e6149f4d-3f2f-485d-ad7b-e4c5f5660f35"));
            #endregion       
        }

        public static async Task CheckMarketAchievements()
        {
            // Throw an exception if the user is not logged in.
            if (GameStateManager.GetCurrentUser() == null) throw new Exception("User must be logged in!");

            #region Buy 15 items from the shop! ec7fafef-4a9b-48bd-8cf2-0a667d63f254
            if (GameStateManager.GetCurrentUser().NrItemsBought >= 15) await AddUserAchievement(Guid.Parse("ec7fafef-4a9b-48bd-8cf2-0a667d63f254"));
            #endregion
        }

        private static async Task AddUserAchievement(Guid achievementId)
        {
            // Throw an exception if the user is not logged in.
            if (GameStateManager.GetCurrentUser() == null) throw new Exception("User must be logged in!");

            // Get all user achievements from the database.
            List<UserAchievement> userAchievements = await UserAchievementRepository.GetAllUserAchievementsAsync(GameStateManager.GetCurrentUserId());

            // Return in case the user has already completed the given achievement.
            foreach (UserAchievement userAchievement in userAchievements)
                if (userAchievement.AchievementId == achievementId) return;

            // Get the achievement from the database.
            Achievement achievement = await AchievementRepository.GetAchievementByIdAsync(achievementId);
            if (achievement == null) throw new Exception($"Achievement with id: {achievementId} not found in the database.");

            // Add the achievement to the user acheivements in the database.
            await UserAchievementRepository.AddUserAchievementAsync(new UserAchievement(
                id: Guid.NewGuid(),
                userId: GameStateManager.GetCurrentUserId(),
                achievementId: achievementId,
                createdTime: DateTime.UtcNow));

            // Update the user coins both in the database and locally.
            User newUser = GameStateManager.GetCurrentUser();
            newUser.Coins += achievement.RewardCoins;
            await UserRepository.UpdateUserAsync(newUser);
            GameStateManager.SetCurrentUser(newUser);

            MessageBox.Show($"Congratulations you unlocked a new achievement! {achievement.Description}");
        }
    }
}
