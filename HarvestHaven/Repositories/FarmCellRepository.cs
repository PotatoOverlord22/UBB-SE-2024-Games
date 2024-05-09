using System.Data;
using HarvestHaven.Entities;
using HarvestHaven.Utils;

namespace HarvestHaven.Repositories
{
    public class FarmCellRepository : IFarmCellRepository
    {
        private readonly IDatabaseProvider databaseProvider;

        public FarmCellRepository(IDatabaseProvider databaseProvider)
        {
            this.databaseProvider = databaseProvider;
        }

        public async Task<List<FarmCell>> GetUserFarmCellsAsync(Guid userId)
        {
            List<FarmCell> farmCells = new List<FarmCell>();
            var parameters = new Dictionary<string, object> { { "@UserId", userId } };

            using (IDataReader reader = await databaseProvider.ExecuteReaderAsync("SELECT * FROM FarmCells WHERE UserId = @UserId", parameters))
            {
                while (reader.Read())
                {
                    farmCells.Add(new FarmCell(
                        id: reader.GetGuid(reader.GetOrdinal("Id")),
                        userId: userId,
                        row: reader.GetInt32(reader.GetOrdinal("Row")),
                        column: reader.GetInt32(reader.GetOrdinal("Column")),
                        itemId: reader.GetGuid(reader.GetOrdinal("ItemId")),
                        lastTimeEnhanced: reader.IsDBNull(reader.GetOrdinal("LastTimeEnhanced")) ? null : reader.GetDateTime(reader.GetOrdinal("LastTimeEnhanced")),
                        lastTimeInteracted: reader.IsDBNull(reader.GetOrdinal("LastTimeInteracted")) ? null : reader.GetDateTime(reader.GetOrdinal("LastTimeInteracted"))));
                }
            }
            return farmCells;
        }

        public async Task<FarmCell> GetUserFarmCellByPositionAsync(Guid userId, int row, int column)
        {
            FarmCell farmCell = null;
            var parameters = new Dictionary<string, object>
            {
                { "@UserId", userId },
                { "@Row", row },
                { "@Column", column }
            };

            using (IDataReader reader = await databaseProvider.ExecuteReaderAsync("SELECT * FROM FarmCells WHERE UserId = @UserId AND Row = @Row AND [Column] = @Column", parameters))
            {
                if (reader.Read())
                {
                    farmCell = new FarmCell(
                        id: reader.GetGuid(reader.GetOrdinal("Id")),
                        userId: userId,
                        row: row,
                        column: column,
                        itemId: reader.GetGuid(reader.GetOrdinal("ItemId")),
                        lastTimeEnhanced: reader.IsDBNull(reader.GetOrdinal("LastTimeEnhanced")) ? null : reader.GetDateTime(reader.GetOrdinal("LastTimeEnhanced")),
                        lastTimeInteracted: reader.IsDBNull(reader.GetOrdinal("LastTimeInteracted")) ? null : reader.GetDateTime(reader.GetOrdinal("LastTimeInteracted")));
                }
            }
            return farmCell;
        }

        public async Task AddFarmCellAsync(FarmCell farmCell)
        {
            var parameters = new Dictionary<string, object>
            {
                { "@Id", farmCell.Id },
                { "@UserId", farmCell.UserId },
                { "@Row", farmCell.Row },
                { "@Column", farmCell.Column },
                { "@ItemId", farmCell.ItemId },
                { "@LastTimeEnhanced", farmCell.LastTimeEnhanced ?? (object)DBNull.Value },
                { "@LastTimeInteracted", farmCell.LastTimeInteracted ?? (object)DBNull.Value }
            };

            await databaseProvider.ExecuteReaderAsync("INSERT INTO FarmCells (Id, UserId, Row, [Column], ItemId, LastTimeEnhanced, LastTimeInteracted) VALUES (@Id, @UserId, @Row, @Column, @ItemId, @LastTimeEnhanced, @LastTimeInteracted)", parameters);
        }

        public async Task UpdateFarmCellAsync(FarmCell farmCell)
        {
            var parameters = new Dictionary<string, object>
            {
                { "@Id", farmCell.Id },
                { "@Row", farmCell.Row },
                { "@Column", farmCell.Column },
                { "@ItemId", farmCell.ItemId },
                { "@LastTimeEnhanced", farmCell.LastTimeEnhanced ?? (object)DBNull.Value },
                { "@LastTimeInteracted", farmCell.LastTimeInteracted ?? (object)DBNull.Value }
            };

            await databaseProvider.ExecuteReaderAsync("UPDATE FarmCells SET Row = @Row, [Column] = @Column, ItemId = @ItemId, LastTimeEnhanced = @LastTimeEnhanced, LastTimeInteracted = @LastTimeInteracted WHERE Id = @Id", parameters);
        }

        public async Task DeleteFarmCellAsync(Guid farmCellId)
        {
            var parameters = new Dictionary<string, object> { { "@Id", farmCellId } };

            await databaseProvider.ExecuteReaderAsync("DELETE FROM FarmCells WHERE Id = @Id", parameters);
        }
    }
}
