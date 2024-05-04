namespace HarvestHaven.Entities
{
    public class MarketBuyItem
    {
        public Guid Id { get; set; }
        public Guid ItemId { get; set; }
        public int BuyPrice { get; set; }

        public MarketBuyItem(Guid id, Guid itemId, int buyPrice)
        {
            Id = id;
            ItemId = itemId;
            BuyPrice = buyPrice;
        }
    }
}
