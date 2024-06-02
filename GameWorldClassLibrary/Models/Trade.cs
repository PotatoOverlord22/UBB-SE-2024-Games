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
    }
}
