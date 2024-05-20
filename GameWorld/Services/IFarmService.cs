using GameWorld.Entities;

namespace GameWorld.Services
{
    public interface IFarmService
    {
        Task<Dictionary<FarmCell, Item>> GetAllFarmCellsForUser(Guid userId);

        Task InteractWithCell(int row, int column);

        Task DestroyCell(int row, int column);

        Task EnchanceCellForUser(Guid targetUserId, int row, int column);

        string GetPicturePathByItemType(ItemType type);
    }
}
