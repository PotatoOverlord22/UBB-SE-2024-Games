namespace GameWorldClassLibrary.Models
{
    public class Trade
    {
        public Guid Id { get; set; }
        public User? User { get; set; }
        public Resource? ResourceToGive { get; set; }
        public int ResourceToGiveQuantity { get; set; }
        public Resource? ResourceToGetResource { get; set; }
        public int ResourceToGetQuantity { get; set; }
        public DateTime TradeCreationTime { get; set; }
        public bool IsCompleted { get; set; }

        public Trade() { }

        public Trade(Guid id, User? user, Resource? resourceToGive, int resourceToGiveQuantity, Resource? resourceToGetResource, int resourceToGetQuantity, DateTime tradeCreationTime, bool isCompleted)
        {
            Id = id;
            User = user;
            ResourceToGive = resourceToGive;
            ResourceToGiveQuantity = resourceToGiveQuantity;
            ResourceToGetResource = resourceToGetResource;
            ResourceToGetQuantity = resourceToGetQuantity;
            TradeCreationTime = tradeCreationTime;
            IsCompleted = isCompleted;
        }
    }
}
