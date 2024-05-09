namespace HarvestHaven.Entities
{
    public class Trade
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ResourceToGiveId { get; set; }
        public int ResourceToGiveQuantity { get; set; }
        public Guid ResourceToGetResourceId { get; set; }
        public int ResourceToGetQuantity { get; set; }
        public DateTime TradeCreationTime { get; set; }
        public bool IsCompleted { get; set; }

        public Trade(Guid id, Guid userId, Guid givenResourceId, int givenResourceQuantity, Guid requestedResourceId, int requestedResourceQuantity, DateTime createdTime, bool isCompleted)
        {
            Id = id;
            UserId = userId;
            ResourceToGiveId = givenResourceId;
            ResourceToGiveQuantity = givenResourceQuantity;
            ResourceToGetResourceId = requestedResourceId;
            ResourceToGetQuantity = requestedResourceQuantity;
            TradeCreationTime = createdTime;
            IsCompleted = isCompleted;
        }
    }
}