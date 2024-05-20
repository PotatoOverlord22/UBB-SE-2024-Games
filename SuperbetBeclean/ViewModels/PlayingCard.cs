namespace SuperbetBeclean.Model
{
    public class PlayingCard
    {
        private string value;
        private string suit;

        public static readonly Dictionary<string, string> CARD_VALUES = new Dictionary<string, string>
        {
            { "2", "2" },
            { "3", "3" },
            { "4", "4" },
            { "5", "5" },
            { "6", "6" },
            { "7", "7" },
            { "8", "8" },
            { "9", "9" },
            { "10", "10" },
            { "J", "J" },
            { "Q", "Q" },
            { "K", "K" },
            { "A", "A" }
        };
        public const string HEART_SYMBOL = "H";
        public const string DIAMOND_SYMBOL = "D";
        public const string SPADE_SYMBOL = "S";
        public const string CLUB_SYMBOL = "C";

        public PlayingCard(string value, string suit)
        {
            this.value = value;
            this.suit = suit;
        }

        public string Value
        {
            get { return value; } set { this.value = value; }
        }
        public string Suit
        {
            get { return suit; } set { suit = value; }
        }
        public string CompleteInformation()
        {
            return value + suit;
        }
    }
}
