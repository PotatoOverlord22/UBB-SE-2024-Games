using Server.API.Models;

namespace Server.API.Services
{
    public interface ITradeRepository
    {
        Task<Trade> GetTradeByIdAsync(Guid id);
        Task<List<Trade>> GetTradesAsync();
        Task AddTradeAsync(Trade trade);
        Task DeleteTradeAsync(Guid id);
        Task UpdateTradeAsync(Guid id, Trade trade);
    }
}