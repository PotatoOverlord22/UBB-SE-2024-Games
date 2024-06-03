namespace GameWorldClassLibrary.Utils
{
    // Maybe move this to a config file?
    public static class Apis
    {
        public static readonly string ACHIEVEMENTS_BASE_URL = "https://localhost:8080/api/Achievements";
        public static readonly string ITEMS_BASE_URL = "https://localhost:8080/api/Items";
        public static readonly string RESOURCES_BASE_URL = "https://localhost:8080/api/Resources";
        public static readonly string INVENTORY_RESOURCES_BASE_URL = "https://localhost:8080/api/InventoryResources";
        public static readonly string MARKET_BUY_ITEM = "https://localhost:8080/api/MarketBuyItem";
        public static readonly string USERS_BASE_URL = "https://localhost:8080/api/Users";
        public static readonly string USERS_USERNAME_URL = "https://localhost:8080/api/Users/username";
        public static readonly string FARM_CELL = "https://localhost:8080/api/FarmCell";
        public static readonly string COMMENTS_BASE_URL = "https://localhost:8080/api/Comments";
        public static readonly string MARKET_SELL_RESOURCE = "https://localhost:8080/api/MarketSellResource";
        public static readonly string POKER_LEADERBOARD_URL = "https://localhost:8080/api/Users/poker-leaderboard";
        public static readonly string TRADES_BASE_URL = "https://localhost:8080/api/Trades";

        public static readonly string TWO_PLAYER_GAMES_BASE_URL = "https://localhost:8080/api/TwoPlayerGames";
        public static readonly string SKILL_ISSUE_BRO_BASE_URL = "https://localhost:8080/api/SkillIssueBro";
    }
}
