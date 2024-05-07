using HarvestHaven.Entities;

namespace HarvestHaven.Services
{
    public interface ITradeService
    {
        Task<List<Trade>> GetAllTradesExceptCreatedByLoggedUser();

        Task<Trade> GetUserTradeAsync(Guid userId);

        Task CreateTradeAsync(ResourceType givenResourceType, int givenResourceQuantity, ResourceType requestedResourceType, int requestedResourceQuantity);

        Task PerformTradeAsync(Guid tradeId);

        Task CancelTradeAsync(Guid tradeId);
    }
}
