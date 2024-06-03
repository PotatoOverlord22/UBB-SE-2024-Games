namespace GameWorldClassLibrary.Models
{
    public class PlayerStats
    {
        private Player player;
        private int trophies;
        private int averageElo;
        private string rank;
        private Games? favoriteGame;

        public Player Player { get => player; set => player = value; }
        public int Trophies { get => trophies; set => trophies = value; }
        public int AverageElo { get => averageElo; set => averageElo = value; }
        public string Rank { get => rank; set => rank = value; }
        public Games FavoriteGame { get => favoriteGame; set => favoriteGame = value; }
        public PlayerStats(Player player, int trophies, int averageElo, string rank, Games favoriteGame)
        {
            this.player = player;
            this.trophies = trophies;
            this.averageElo = averageElo;
            this.rank = rank;
            this.favoriteGame = favoriteGame;
        }

        public PlayerStats(Player player)
        {
            this.player = player;
            this.trophies = 0;
            this.averageElo = 0;
            rank = "";
            favoriteGame = null;
        }
    }
}
