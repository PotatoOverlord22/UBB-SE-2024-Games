using GameWorldClassLibrary.Models;

namespace GameWorldClassLibrary.Services.Interfaces
{
    public interface IMarketService
    {
        Task BuyItem(int row, int column, ItemType itemType);

        Task SellResource(ResourceType resourceType);
    }
}
