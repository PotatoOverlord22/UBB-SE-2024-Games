using GameWorld.Models;

namespace GameWorld.Repositories
{
    public interface ITradeRepository
    {
        Task<List<Trade>> GetAllTradesAsync();
        Task<List<Trade>> GetAllTradesExceptCreatedByUser(Guid userId);
        Task<Trade> GetTradeByIdAsync(Guid tradeId);
        Task<Trade> GetUserTradeAsync(Guid userId);
        Task CreateTradeAsync(Trade trade);
        Task UpdateTradeAsync(Trade trade);
        Task DeleteTradeAsync(Guid tradeId);
    }
}
