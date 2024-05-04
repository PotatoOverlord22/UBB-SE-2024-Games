namespace HarvestHaven.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public int Coins { get; set; }
        public int NrItemsBought { get; set; }
        public int NrTradesPerformed { get; set; }
        public DateTime? TradeHallUnlockTime { get; set; } // Nullable.
        public DateTime? LastTimeReceivedWater { get; set; } // Nullable.

        public User(Guid id, string username, int coins, int nrItemsBought, int nrTradesPerformed, DateTime? tradeHallUnlockTime, DateTime? lastTimeReceivedWater)
        {
            Id = id;
            Username = username;
            Coins = coins;
            NrItemsBought = nrItemsBought;
            NrTradesPerformed = nrTradesPerformed;
            TradeHallUnlockTime = tradeHallUnlockTime;
            LastTimeReceivedWater = lastTimeReceivedWater;
        }
    }
}
