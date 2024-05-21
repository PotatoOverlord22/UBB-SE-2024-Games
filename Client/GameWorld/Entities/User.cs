namespace GameWorld.Entities
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
        public int UserCurrentFont { get; set; }
        public int UserCurrentTitle { get; set; }
        public string UserCurrentIconPath { get; set; }
        public int UserCurrentTable { get; set; }
        public int UserChips { get; set; }
        public int UserStack { get; set; }
        public int UserStreak { get; set; }
        public int UserHandsPlayed { get; set; }
        public int UserLevel { get; set; }
        public int UserStatus { get; set; } // Inactive, Waiting, Playing
        public int UserBet { get; set; }
        public int UserTablePlace { get; set; }
        public DateTime UserLastLogin { get; set; }
        public PlayingCard[] UserCurrentHand { get; set; }

        // From SuperbetBeclean
        // private const int DEFAULT_USER_ID = 0;
        // private const string DEFAULT_USER_NAME = "";
        private const int DEFAULT_USER_CURRENT_FONT = 0;
        private const int DEFAULT_USER_CURRENT_TITLE = 0;
        private const string DEFAULT_USER_CURRENT_ICON_PATH = "";
        private const int DEFAULT_USER_CURRENT_TABLE = 0;
        private const int DEFAULT_USER_CHIPS = 0;
        private const int DEFAULT_USER_STACK = 0;
        private const int DEFAULT_USER_STREAK = 0;
        private const int DEFAULT_USER_HANDS_PLAYED = 0;
        private const int DEFAULT_USER_LEVEL = 0;
        private const int DEFAULT_USER_STATUS = 0;
        private const int DEFAULT_USER_BET = 0;
        private const int DEFAULT_USER_TABLE_PLACE = 0;
        private const int STARTING_HAND_SIZE = 2;

        public User(Guid id, string username, int coins = 0, int nrItemsBought = 0, int nrTradesPerformed = 0,
            DateTime? tradeHallUnlockTime = null, DateTime? lastTimeReceivedWater = null, int userCurrentFont = DEFAULT_USER_CURRENT_FONT,
            int userCurrentTitle = DEFAULT_USER_CURRENT_TITLE, string userCurrentIconPath = DEFAULT_USER_CURRENT_ICON_PATH,
            int userCurrentTable = DEFAULT_USER_CURRENT_TABLE, int userChips = DEFAULT_USER_CHIPS,
            int userStack = DEFAULT_USER_STACK, int userStreak = DEFAULT_USER_STREAK,
            int userHandsPlayed = DEFAULT_USER_HANDS_PLAYED, int userLevel = DEFAULT_USER_LEVEL,
            int userStatus = DEFAULT_USER_STATUS, int userBet = DEFAULT_USER_BET,
            int userTablePlace = DEFAULT_USER_TABLE_PLACE, DateTime userLastLogin = default(DateTime))
        {
            Id = id;
            Username = username;
            Coins = coins;
            AmountOfItemsBought = nrItemsBought;
            AmountOfTradesPerformed = nrTradesPerformed;
            TradeHallUnlockTime = tradeHallUnlockTime;
            LastTimeReceivedWater = lastTimeReceivedWater;
            UserCurrentFont = userCurrentFont;
            UserCurrentTitle = userCurrentTitle;
            UserCurrentIconPath = userCurrentIconPath;
            UserCurrentTable = userCurrentTable;
            UserChips = userChips;
            UserStack = userStack;
            UserStreak = userStreak;
            UserHandsPlayed = userHandsPlayed;
            UserLevel = userLevel;
            UserStatus = userStatus;
            UserBet = userBet;
            UserTablePlace = userTablePlace;
            UserLastLogin = userLastLogin;
            UserCurrentHand = new PlayingCard[STARTING_HAND_SIZE];
        }
    }
}
