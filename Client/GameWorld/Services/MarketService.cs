using GameWorldClassLibrary.Models;
using GameWorld.Repositories;
using GameWorld.Resources.Utils;

namespace GameWorld.Services
{
    public class MarketService : ServiceBase, IMarketService
    {
        private readonly IAchievementService achievementService;
        private readonly IFarmCellRepository farmCellRepository;
        private readonly IUserRepository userRepository;
        private readonly IItemRepository itemRepository;
        private readonly IMarketBuyItemRepository marketBuyItemRepository;
        private readonly IMarketSellResourceRepository marketSellResourceRepository;
        private readonly IInventoryResourceRepository inventoryResourceRepository;
        private readonly IResourceRepository resourceRepository;
        private int userCoins;
        public int UserCurrentCoins
        {
            get
            {
                return userCoins;
            }

            set
            {
                userCoins = value;
                OnPropertyChanged();
            }
        }

        public MarketService(IAchievementService achievementService, IFarmCellRepository farmCellRepository, IUserRepository userRepository, IItemRepository itemRepository, IMarketBuyItemRepository marketBuyItemRepository, IInventoryResourceRepository inventoryResourceRepository, IMarketSellResourceRepository marketSellResourceRepository, IResourceRepository resourceRepository)
        {
            this.achievementService = achievementService;
            this.farmCellRepository = farmCellRepository;
            this.userRepository = userRepository;
            this.itemRepository = itemRepository;
            this.marketBuyItemRepository = marketBuyItemRepository;
            this.inventoryResourceRepository = inventoryResourceRepository;
            this.marketSellResourceRepository = marketSellResourceRepository;
            this.resourceRepository = resourceRepository;

            User? user = GameStateManager.GetCurrentUser();
            if (user != null)
            {
                UserCurrentCoins = user.Coins;
            }
        }
        public async Task BuyItem(int row, int column, ItemType itemType)
        {
            // Throw an exception if the user is not logged in.
            if (GameStateManager.GetCurrentUser() == null)
            {
                throw new Exception("User must be logged in!");
            }

            // Get the item from the database.
            Item item = await itemRepository.GetItemByTypeAsync(itemType);
            if (item == null)
            {
                throw new Exception("Given item not found in the database.");
            }

            // Get the corresponding market buy item from the database.
            MarketBuyItem marketBuyItem = await marketBuyItemRepository.GetMarketBuyItemByItemIdAsync(item.Id);
            if (marketBuyItem == null)
            {
                throw new Exception("Market buy item not found in the database.");
            }

            // Throw an exception if the user doesn't have enough money to buy the item.
            if (GameStateManager.GetCurrentUser().Coins < marketBuyItem.BuyPrice)
            {
                throw new Exception("Not enough money!");
            }

            // Get all the user farm cells from the database.
            List<FarmCell> farmCells = await farmCellRepository.GetUserFarmCellsAsync(GameStateManager.GetCurrentUserId());

            // Throw an exception in case the cell is occupied.
            foreach (FarmCell cell in farmCells)
            {
                if (cell.Row == row && cell.Column == column)
                {
                    throw new Exception("Cell is occupied.");
                }
            }

            // Add a new farm cell in the database.
            await farmCellRepository.AddFarmCellAsync(new FarmCell(
                id: Guid.NewGuid(),
                user: GameStateManager.GetCurrentUser(),
                row: row,
                column: column,
                item: item,
                lastTimeEnhanced: null,
                lastTimeInteracted: null));

            // Update the user coins and number of items bought both locally and in the database.
            User newUser = GameStateManager.GetCurrentUser();
            newUser.Coins -= marketBuyItem.BuyPrice;
            newUser.AmountOfItemsBought++;
            await userRepository.UpdateUserAsync(newUser);
            GameStateManager.SetCurrentUser(newUser);

            UserCurrentCoins = newUser.Coins;

            // Check achievements.
            await achievementService.CheckFarmAchievements();
            await achievementService.CheckMarketAchievements();
        }

        public async Task SellResource(ResourceType resourceType)
        {
            // Throw an exception if the user is not logged in.
            if (GameStateManager.GetCurrentUser() == null)
            {
                throw new Exception("User must be logged in!");
            }

            // Get the resource from the database.
            Resource resource = await resourceRepository.GetResourceByTypeAsync(resourceType);
            if (resource == null)
            {
                throw new Exception("Given resource not found.");
            }

            // Get the market sell resource from the database.
            MarketSellResource marketSellResouce = await marketSellResourceRepository.GetMarketSellResourceByResourceIdAsync(resource.Id);
            if (marketSellResouce == null)
            {
                throw new Exception("Market sell resource not found.");
            }

            // Get the user's inventory resource from the database.
            InventoryResource? inventoryResource = await inventoryResourceRepository.GetUserResourceByResourceIdAsync(GameStateManager.GetCurrentUserId(), resource.Id);

            // Throw an exception if the user doesn's have that resource.
            if (inventoryResource == null || inventoryResource.Quantity <= 0)
            {
                throw new Exception("You do not own any " + resource.ResourceType.ToString() + "!");
            }

            // Update the inventory resource quantity in the database.
            inventoryResource.Quantity--;
            await inventoryResourceRepository.UpdateUserResourceAsync(inventoryResource);

            // Update the user coins both locally and in the database.
            User newUser = GameStateManager.GetCurrentUser();
            newUser.Coins += marketSellResouce.SellPrice;
            await userRepository.UpdateUserAsync(newUser);
            GameStateManager.SetCurrentUser(newUser);

            UserCurrentCoins = newUser.Coins;

            // Check achievements.
            await achievementService.CheckInventoryAchievements();
        }
    }
}
