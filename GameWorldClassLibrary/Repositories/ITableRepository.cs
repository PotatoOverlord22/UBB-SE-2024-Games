using GameWorldClassLibrary.Models;

namespace GameWorldClassLibrary.Repositories
{
    public interface ITableRepository
    {
        Task<Table> GetTableByIdAsync(Guid id);
        Task<List<Table>> GetTableAsync();
        Task AddTableAsync(Table table);
        Task DeleteTableAsync(Guid id);
        Task UpdateTableAsync(Guid id, Table table);
    }
}