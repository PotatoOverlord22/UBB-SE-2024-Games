using GameWorldClassLibrary.Models;
using GameWorldClassLibrary.Utils;

namespace GameWorldClassLibrary.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IUserService userService;
        private Dictionary<InventoryResource, Resource> resources;
        public InventoryService(IUserService userService)
        {
            this.userService = userService;
            resources = new Dictionary<InventoryResource, Resource>();
        }
        public async Task<string> GetCorrespondingValueForLabel(string labelName)
        {
            resources = await userService.GetInventoryResources(GameStateManager.GetCurrentUserId());

            switch (labelName)
            {
                case "carrotLabel":
                    return GetResourceQuantity(ResourceType.Carrot);
                case "cornLabel":
                    return GetResourceQuantity(ResourceType.Corn);
                case "wheatLabel":
                    return GetResourceQuantity(ResourceType.Wheat);
                case "tomatoLabel":
                    return GetResourceQuantity(ResourceType.Tomato);
                case "chickenLabel":
                    return GetResourceQuantity(ResourceType.ChickenMeat);
                case "sheepLabel":
                    return GetResourceQuantity(ResourceType.Mutton);
                case "chickenEggLabel":
                    return GetResourceQuantity(ResourceType.ChickenEgg);
                case "woolLabel":
                    return GetResourceQuantity(ResourceType.SheepWool);
                case "milkLabel":
                    return GetResourceQuantity(ResourceType.CowMilk);
                case "duckEggLabel":
                    return GetResourceQuantity(ResourceType.DuckEgg);
                case "cowLabel":
                    return GetResourceQuantity(ResourceType.Steak);
                case "duckLabel":
                    return GetResourceQuantity(ResourceType.DuckMeat);
            }
            return "0";
        }

        private string GetResourceQuantity(ResourceType resourceType)
        {
            foreach (var entry in resources)
            {
                if (entry.Value.ResourceType == resourceType)
                {
                    return entry.Key.Quantity.ToString();
                }
            }

            return "0"; // If resource type not found, return "0"
        }
    }
}
