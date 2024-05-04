namespace HarvestHaven.Entities
{
    public class Trade
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid GivenResourceId { get; set; }
        public int GivenResourceQuantity { get; set; }
        public Guid RequestedResourceId { get; set; }
        public int RequestedResourceQuantity { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsCompleted { get; set; }

        public Trade(Guid id, Guid userId, Guid givenResourceId, int givenResourceQuantity, Guid requestedResourceId, int requestedResourceQuantity, DateTime createdTime, bool isCompleted)
        {
            Id = id;
            UserId = userId;
            GivenResourceId = givenResourceId;
            GivenResourceQuantity = givenResourceQuantity;
            RequestedResourceId = requestedResourceId;
            RequestedResourceQuantity = requestedResourceQuantity;
            CreatedTime = createdTime;
            IsCompleted = isCompleted;
        }
    }
}