using GameWorld.Models;

namespace GameWorld.Services
{
    public interface IMarketService
    {
        Task BuyItem(int row, int column, ItemType itemType);

        Task SellResource(ResourceType resourceType);
    }
}
