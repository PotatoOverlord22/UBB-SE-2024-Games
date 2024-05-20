namespace SuperbetBeclean.Model
{
    public class User
    {
        private int userID;
        private string userName;
        private int userCurrentFont;
        private int userCurrentTitle;
        private string userCurrentIconPath;
        private int userCurrentTable;
        private int userChips;
        private int userStack;
        private int userStreak;
        private int userHandsPlayed;
        private int userLevel;
        private int userStatus; // Inactive, Waiting, Playing
        private int userBet;
        private int userTablePlace;
        private DateTime userLastLogin;
        private const int DEFAULT_USER_ID = 0;
        private const string DEFAULT_USER_NAME = "";
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

        private PlayingCard[] userCurrentHand;

        public User(int userID = DEFAULT_USER_ID,
            string userName = DEFAULT_USER_NAME,
            int userCurrentFont = DEFAULT_USER_CURRENT_FONT,
            int userCurrentTitle = DEFAULT_USER_CURRENT_TITLE,
            string userCurrentIconPath = DEFAULT_USER_CURRENT_ICON_PATH,
            int userCurrentTable = DEFAULT_USER_CURRENT_TABLE,
            int userChips = DEFAULT_USER_CHIPS,
            int userStack = DEFAULT_USER_STACK,
            int userStreak = DEFAULT_USER_STREAK,
            int userHandsPlayed = DEFAULT_USER_HANDS_PLAYED,
            int userLevel = DEFAULT_USER_LEVEL,
            DateTime userLastLogin = default(DateTime))
        {
            this.userID = userID;
            this.userName = userName;
            this.userCurrentFont = userCurrentFont;
            this.userCurrentTitle = userCurrentTitle;
            this.userCurrentIconPath = userCurrentIconPath;
            this.userCurrentTable = userCurrentTable;
            this.userChips = userChips;
            this.userStack = userStack;
            this.userStreak = userStreak;
            this.userHandsPlayed = userHandsPlayed;
            this.userLevel = userLevel;
            this.userLastLogin = userLastLogin;
            userStatus = DEFAULT_USER_STATUS;
            userBet = DEFAULT_USER_BET;
            userCurrentHand = new PlayingCard[STARTING_HAND_SIZE];
            userTablePlace = DEFAULT_USER_TABLE_PLACE;
        }

        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        public int UserCurrentFont
        {
            get { return userCurrentFont; }
            set { userCurrentFont = value; }
        }
        public int UserCurrentTitle
        {
            get { return userCurrentTitle; }
            set { userCurrentTitle = value; }
        }
        public string UserCurrentIconPath
        {
            get { return userCurrentIconPath; }
            set { userCurrentIconPath = value; }
        }
        public int UserCurrentTable
        {
            get { return userCurrentTable; }
            set { userCurrentTable = value; }
        }
        public int UserChips
        {
            get { return userChips; }
            set { userChips = value; }
        }
        public int UserStack
        {
            get { return userStack; }
            set { userStack = value; }
        }
        public int UserStreak
        {
            get { return userStreak; }
            set { userStreak = value; }
        }
        public int UserHandsPlayed
        {
            get { return userHandsPlayed; }
            set { userHandsPlayed = value; }
        }
        public int UserLevel
        {
            get { return userLevel; }
            set { userLevel = value; }
        }
        public DateTime UserLastLogin
        {
            get { return userLastLogin; }
            set { userLastLogin = value; }
        }
        public int UserStatus
        {
            get { return userStatus; }
            set { userStatus = value; }
        }
        public int UserBet
        {
            get { return userBet; }
            set { userBet = value; }
        }
        public int UserTablePlace
        {
            get { return userTablePlace; }
            set { userTablePlace = value; }
        }
        public PlayingCard[] UserCurrentHand
        {
            get { return userCurrentHand; }
            set { userCurrentHand = value; }
        }
    }
}
