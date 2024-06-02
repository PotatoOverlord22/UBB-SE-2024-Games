namespace GameWorldClassLibrary.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public int Coins { get; set; }
        public int AmountOfItemsBought { get; set; }
        public int AmountOfTradesPerformed { get; set; }
        public DateTime? TradeHallUnlockTime { get; set; } // Nullable.
        public DateTime? LastTimeReceivedWater { get; set; } // Nullable.

        // From SuperbetBeclean
        public Font UserCurrentFont { get; set; }
        public Title UserCurrentTitle { get; set; }
        public string UserCurrentIconPath { get; set; }
        public Table UserCurrentTable { get; set; }
        public int UserChips { get; set; }
        public int UserStack { get; set; }
        public int UserStreak { get; set; }
        public int UserHandsPlayed { get; set; }
        public int UserLevel { get; set; }
        public int UserStatus { get; set; } // Inactive, Waiting, Playing
        public int UserBet { get; set; }
        public int UserTablePlace { get; set; }
        public DateTime UserLastLogin { get; set; }
        public List<PlayingCard> UserCurrentHand { get; set; }
    }
}