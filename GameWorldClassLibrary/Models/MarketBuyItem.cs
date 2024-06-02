namespace GameWorldClassLibrary.Models
{
    public class MarketBuyItem
    {
        public Guid Id { get; set; }
        public Item Item { get; set; }
        public int BuyPrice { get; set; }
    }
}
