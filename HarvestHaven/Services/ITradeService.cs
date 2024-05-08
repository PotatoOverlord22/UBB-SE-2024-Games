using HarvestHaven.Entities;

namespace HarvestHaven.Services
{
    public interface ITradeService
    {
        Task<List<Trade>> GetAllTradesExceptCreatedByLoggedUser();

        Task<Trade> GetUserTradeAsync(Guid userId);

        Task CreateTradeAsync(ResourceType givenResourceType, string givenResourceQuantity, ResourceType requestedResourceType, string requestedResourceQuantity);

        Task PerformTradeAsync(Guid tradeId);

        Task CancelTradeAsync(Guid tradeId);
        string GetPicturePathByResourceType(ResourceType resourceType);
    }
}
