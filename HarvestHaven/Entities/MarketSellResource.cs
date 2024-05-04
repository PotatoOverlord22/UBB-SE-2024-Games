namespace HarvestHaven.Entities
{
    public class MarketSellResource
    {
        public Guid Id { get; set; }
        public Guid ResourceId { get; set; }
        public int SellPrice { get; set; }

        public MarketSellResource(Guid id, Guid resourceId, int sellPrice)
        {
            Id = id;
            ResourceId = resourceId;
            SellPrice = sellPrice;
        }
    }
}
