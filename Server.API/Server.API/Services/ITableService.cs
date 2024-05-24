using Server.API.Models;

namespace Server.API.Services
{
    public interface ITableService
    {
        Task<Table> GetTableByIdAsync(Guid id);
        Task<List<Table>> GetTableAsync();
        Task AddTableAsync(Table table);
        Task DeleteTableAsync(Guid id);
        Task UpdateTableAsync(Guid id, Table table);
    }
}