namespace GameWorld.Models
{
    public class MarketSellResource
    {
        public Guid Id { get; set; }
        public Resource ResourceToSell { get; set; }
        public int SellPrice { get; set; }

        public MarketSellResource(Guid id, Resource resourceToSell, int sellPrice)
        {
            Id = id;
            ResourceToSell = resourceToSell;
            SellPrice = sellPrice;
        }
    }
}
