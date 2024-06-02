using Microsoft.EntityFrameworkCore;
using GameWorldClassLibrary.Models;
using GameWorldClassLibrary.Utils;

namespace GameWorldClassLibrary.Repositories
{
    public class FarmCellRepository : IFarmCellRepository
    {
        private GamesContext gameContext;

        public FarmCellRepository(GamesContext gameContext)
        {
            this.gameContext = gameContext;
        }

        public async Task AddFarmCellAsync(FarmCell farmCell)
        {
            gameContext.FarmCells.Add(farmCell);
            await gameContext.SaveChangesAsync();
        }

        public async Task DeleteFarmCellAsync(Guid farmCellId)
        {
            var userResource = gameContext.FarmCells.Find(farmCellId) ?? throw new KeyNotFoundException("Farm Cell not found");
            gameContext.FarmCells.Remove(userResource);
            await gameContext.SaveChangesAsync();
        }

        public async Task<FarmCell> GetUserFarmCellByPositionAsync(Guid userId, int row, int column)
        {
            var userFarmCell = await gameContext.FarmCells
                                     .FirstOrDefaultAsync(ir => ir.User.Id == userId && ir.Column == column && ir.Row == row) ?? throw new KeyNotFoundException("Farm Cell not found");
            return userFarmCell;
        }

        public async Task<List<FarmCell>> GetUserFarmCellsAsync(Guid userId)
        {
            return await gameContext.FarmCells
                        .Where(ir => ir.User.Id == userId)
                        .ToListAsync();
        }

        public async Task UpdateFarmCellAsync(FarmCell farmCell)
        {
            if (gameContext.FarmCells.Find(farmCell.Id) == null)
            {
                throw new KeyNotFoundException("Farm Cell not found");
            }

            gameContext.FarmCells.Update(farmCell);
            await gameContext.SaveChangesAsync();
        }
    }
}
