namespace Server.API.Models
{
    public class MarketSellResource
    {
        public Guid Id { get; set; }
        public Resource ResourceToSell { get; set; }
        public int SellPrice { get; set; }
    }
}
