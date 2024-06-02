using GameWorldClassLibrary.Models;

namespace GameWorldClassLibrary.Repositories
{
    public interface IFarmCellRepository
    {
        Task<List<FarmCell>> GetUserFarmCellsAsync(Guid userId);
        Task<FarmCell> GetUserFarmCellByPositionAsync(Guid userId, int row, int column);
        Task AddFarmCellAsync(FarmCell farmCell);
        Task UpdateFarmCellAsync(FarmCell farmCell);
        Task DeleteFarmCellAsync(Guid farmCellId);
    }
}
