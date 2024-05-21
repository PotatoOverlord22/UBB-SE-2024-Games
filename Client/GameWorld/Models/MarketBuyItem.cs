namespace GameWorld.Models
{
    public class MarketBuyItem
    {
        public Guid Id { get; set; }
        public Item Item { get; set; }
        public int BuyPrice { get; set; }

        public MarketBuyItem(Guid id, Item item, int buyPrice)
        {
            Id = id;
            Item = item;
            BuyPrice = buyPrice;
        }
    }
}
