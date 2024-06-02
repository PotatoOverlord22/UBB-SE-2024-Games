using System.Windows;
using GameWorldClassLibrary.Models;
using GameWorldClassLibrary.Repositories;
using GameWorldClassLibrary.Services;
using GameWorldClassLibrary.Utils;

namespace GameWorldClassLibrary.Services
{
    public class AchievementService : IAchievementService
    {
        private readonly IUserService userService;
        private readonly IAchievementRepository achievementRepository;
        private readonly IUserAchievementRepository userAchievementRepository;
        private readonly IFarmCellRepository farmCellRepository;
        private readonly IUserRepository userRepository;
        private readonly IItemRepository itemRepository;

        public AchievementService(IUserService userService, IAchievementRepository achievementRepository, IUserAchievementRepository userAchievementRepository, IFarmCellRepository farmCellRepository, IUserRepository userRepository, IItemRepository itemRepository)
        {
            this.userService = userService;
            this.achievementRepository = achievementRepository;
            this.userAchievementRepository = userAchievementRepository;
            this.farmCellRepository = farmCellRepository;
            this.userRepository = userRepository;
            this.itemRepository = itemRepository;
        }

        public async Task<List<Achievement>> GetAllAchievementsAsync()
        {
            // Throw an exception if the user is not logged in.
            if (GameStateManager.GetCurrentUser() == null)
            {
                throw new Exception("User must be logged in!");
            }

            return await achievementRepository.GetAllAchievementsAsync();
        }

        public async Task<Dictionary<UserAchievement, Achievement>> GetUserAchievements()
        {
            // Throw an exception if the user is not logged in.
            if (GameStateManager.GetCurrentUser() == null)
            {
                throw new Exception("User must be logged in!");
            }

            // Get all user achievements from the database.
            List<UserAchievement> userAchievements = await userAchievementRepository.GetAllUserAchievementsAsync(GameStateManager.GetCurrentUserId());

            // Initialize the dictionary that will be returned.
            Dictionary<UserAchievement, Achievement> userAchievementsMap = new Dictionary<UserAchievement, Achievement>();

            // Go through each user achievement.
            foreach (UserAchievement userAchievement in userAchievements)
            {
                // Get the corresponding achievement from the database.
                Achievement achievement = await achievementRepository.GetAchievementByIdAsync(userAchievement.User.Id);
                if (achievement == null)
                {
                    throw new Exception($"No corresponding achievement found for the user achievement with id: {userAchievement.Id}");
                }

                // Add the pair in the dictionary.
                userAchievementsMap.Add(userAchievement, achievement);
            }

            return userAchievementsMap;
        }

        public async Task CheckFarmAchievements()
        {
            // Throw an exception if the user is not logged in.
            if (GameStateManager.GetCurrentUser() == null)
            {
                throw new Exception("User must be logged in!");
            }

            // Get all the user farm cells.
            List<FarmCell> farmCells = await farmCellRepository.GetUserFarmCellsAsync(GameStateManager.GetCurrentUserId());

            async Task<bool> IsCowAtPosition(int row, int column, List<FarmCell> farmCells)
            {
                // Go throguh all the farm cells.
                foreach (FarmCell cell in farmCells)
                {
                    // Skip the ones that do not match the given row and the column.
                    if (cell.Row != row || cell.Column != column)
                    {
                        continue;
                    }

                    // Get the item from the cell.
                    Item cellItem = await itemRepository.GetItemByIdAsync(cell.Item.Id);

                    return cellItem?.ItemType == ItemType.Cow;
                }

                // Return false if the cell is not found.
                return false;
            }

            // Go through all the farm cells.
            foreach (FarmCell cell in farmCells)
            {
                // Get the item from the cell and skip null items or non-cow items.
                Item item = await itemRepository.GetItemByIdAsync(cell.Item.Id);
                if (item == null || item.ItemType != ItemType.Cow)
                {
                    continue;
                }

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

            // Initialize the counters.
            int cows = 0, sheeps = 0, ducks = 0, chickens = 0;

            foreach (FarmCell cell in farmCells)
            {
                // Get the item from the cell and skip null items.
                Item item = await itemRepository.GetItemByIdAsync(cell.Item.Id);
                if (item == null)
                {
                    continue;
                }

                switch (item.ItemType)
                {
                    case ItemType.Cow: cows++; break;
                    case ItemType.Sheep: sheeps++; break;
                    case ItemType.Duck: ducks++; break;
                    case ItemType.Chicken: chickens++; break;
                }
            }

            if (cows == sheeps && sheeps == ducks && ducks == chickens && cows > 0)
            {
                await AddUserAchievement(Guid.Parse("a3b0c4bb-2f1b-4189-9be0-0cc25bf65868"));
            }

            // Define the target item types.
            ItemType[] targetItemTypes = { ItemType.CarrotSeeds, ItemType.WheatSeeds, ItemType.TomatoSeeds, ItemType.Sheep, ItemType.Chicken, ItemType.Cow };

            // Create a structure to mark row and column positions.
            bool[,] markedPositions = new bool[Constants.FARM_SIZE + 1, Constants.FARM_SIZE + 1];

            // Go through all the farm cells.
            foreach (FarmCell cell in farmCells)
            {
                // Get the item from the cell and skip null items..
                Item item = await itemRepository.GetItemByIdAsync(cell.Item.Id);
                if (item != null && targetItemTypes.Contains(item.ItemType))
                {
                    // Mark the row and column positions for cells containing one of the target item types.
                    markedPositions[cell.Row, cell.Column] = true;
                }
            }

            // Check if the corners are marked.
            bool allCornersMarked = markedPositions[1, 1] && markedPositions[1, Constants.FARM_SIZE] && markedPositions[Constants.FARM_SIZE, 1] && markedPositions[Constants.FARM_SIZE, Constants.FARM_SIZE];
            if (allCornersMarked)
            {
                await AddUserAchievement(Guid.Parse("37b6e693-c25b-4a1d-ae9e-904148be486c"));
            }
        }

        public async Task CheckTradeAchievements(Guid otherUserInvolvedId)
        {
            // Throw an exception if the user is not logged in.
            if (GameStateManager.GetCurrentUser() == null)
            {
                throw new Exception("User must be logged in!");
            }

            if (GameStateManager.GetCurrentUser().AmountOfTradesPerformed >= 1)
            {
                await AddUserAchievement(Guid.Parse("15a0ffbb-3e0a-4a5e-8a87-4bf35395622e"));
            }

            if (GameStateManager.GetCurrentUser().AmountOfTradesPerformed >= 3)
            {
                await AddUserAchievement(Guid.Parse("8f01cc9d-e620-4719-96af-e3c078a85b4d"));
            }

            if (GameStateManager.GetCurrentUser().AmountOfTradesPerformed >= 5)
            {
                await AddUserAchievement(Guid.Parse("bf6f36e4-718a-4b9e-a991-e02d64257cbf"));
            }

            User otherUser = await userRepository.GetUserByIdAsync(otherUserInvolvedId);
            if (otherUser != null && otherUser.AmountOfTradesPerformed == 1)
            {
                await AddUserAchievement(Guid.Parse("a8f407e3-5338-4bf8-bd20-61bf6fa78aa9"));
            }
        }

        public async Task CheckInventoryAchievements()
        {
            // Throw an exception if the user is not logged in.
            if (GameStateManager.GetCurrentUser() == null)
            {
                throw new Exception("User must be logged in!");
            }

            // Get the neccesary resources from the current user's inventory.
            InventoryResource wheatInventoryResource = await userService.GetInventoryResourceByType(ResourceType.Wheat, GameStateManager.GetCurrentUserId());
            InventoryResource tomatoInventoryResource = await userService.GetInventoryResourceByType(ResourceType.Tomato, GameStateManager.GetCurrentUserId());
            InventoryResource cornInventoryResource = await userService.GetInventoryResourceByType(ResourceType.Corn, GameStateManager.GetCurrentUserId());
            InventoryResource chickenEggsInventoryResource = await userService.GetInventoryResourceByType(ResourceType.ChickenEgg, GameStateManager.GetCurrentUserId());
            InventoryResource duckEggsInventoryResource = await userService.GetInventoryResourceByType(ResourceType.DuckEgg, GameStateManager.GetCurrentUserId());
            InventoryResource chickenMeatInventoryResource = await userService.GetInventoryResourceByType(ResourceType.ChickenMeat, GameStateManager.GetCurrentUserId());
            InventoryResource duckMeatInventoryResource = await userService.GetInventoryResourceByType(ResourceType.DuckMeat, GameStateManager.GetCurrentUserId());

            if (wheatInventoryResource != null && wheatInventoryResource.Quantity == 69)
            {
                await AddUserAchievement(Guid.Parse("bccadd9c-c520-4c6e-8efb-8ef7642edde0"));
            }

            int? sum = chickenEggsInventoryResource?.Quantity + duckEggsInventoryResource?.Quantity;
            if (sum != null && sum == 25)
            {
                await AddUserAchievement(Guid.Parse("be3aba07-1741-4194-b282-103919dcca0f"));
            }

            if (chickenMeatInventoryResource != null && duckMeatInventoryResource != null && chickenMeatInventoryResource.Quantity == duckMeatInventoryResource.Quantity)
            {
                await AddUserAchievement(Guid.Parse("902d847c-c056-4a77-8573-581c22521f7a"));
            }

            if (chickenEggsInventoryResource != null && duckEggsInventoryResource != null && chickenEggsInventoryResource.Quantity == duckEggsInventoryResource.Quantity)
            {
                await AddUserAchievement(Guid.Parse("1bbe52db-494a-4af1-9997-37d4202b7165"));
            }

            if (tomatoInventoryResource?.Quantity == 0 && chickenEggsInventoryResource?.Quantity == 0 || duckEggsInventoryResource?.Quantity == 0 && cornInventoryResource?.Quantity == 0)
            {
                await AddUserAchievement(Guid.Parse("e6149f4d-3f2f-485d-ad7b-e4c5f5660f35"));
            }
        }

        public async Task CheckMarketAchievements()
        {
            // Throw an exception if the user is not logged in.
            if (GameStateManager.GetCurrentUser() == null)
            {
                throw new Exception("User must be logged in!");
            }

            if (GameStateManager.GetCurrentUser().AmountOfItemsBought >= 15)
            {
                await AddUserAchievement(Guid.Parse("ec7fafef-4a9b-48bd-8cf2-0a667d63f254"));
            }
        }

        private async Task AddUserAchievement(Guid achievementId)
        {
            // Throw an exception if the user is not logged in.
            if (GameStateManager.GetCurrentUser() == null)
            {
                throw new Exception("User must be logged in!");
            }

            // Get all user achievements from the database.
            List<UserAchievement> userAchievements = await userAchievementRepository.GetAllUserAchievementsAsync(GameStateManager.GetCurrentUserId());

            // Return in case the user has already completed the given achievement.
            foreach (UserAchievement userAchievement in userAchievements)
            {
                if (userAchievement.Achievement.Id == achievementId)
                {
                    return;
                }
            }

            // Get the achievement from the database.
            Achievement achievement = await achievementRepository.GetAchievementByIdAsync(achievementId);
            if (achievement == null)
            {
                throw new Exception($"Achievement with id: {achievementId} not found in the database.");
            }

            // Add the achievement to the user acheivements in the database.
            await userAchievementRepository.AddUserAchievementAsync(new UserAchievement(
                id: Guid.NewGuid(),
                user: GameStateManager.GetCurrentUser(),
                achievement: await achievementRepository.GetAchievementByIdAsync(achievementId),
                achievementRewardedTime: DateTime.UtcNow));

            // Update the user coins both in the database and locally.
            User newUser = GameStateManager.GetCurrentUser();
            newUser.Coins += achievement.NumberOfCoinsRewarded;
            await userRepository.UpdateUserAsync(newUser);
            GameStateManager.SetCurrentUser(newUser);
        }
    }
}
