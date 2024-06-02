using System.ComponentModel.DataAnnotations.Schema;

namespace GameWorldClassLibrary.Models
{
    public class GameStats
    {
        public Guid Id { get; set; }
        public Guid PlayerId { get; set; }
        [ForeignKey("PlayerId")] // Foreign key to Player2PlayerGame
        public Player Player { get; set; } // Reference to the Player object
        public Guid GameId { get; set; }
        [ForeignKey("GameId")] // Foreign key to Games
        public Games Game { get; set; }
        public int EloRating { get; set; }
        public int HighestElo { get; set; }
        public int TotalMatches { get; set; }
        public int TotalWins { get; set; }
        public int TotalDraws { get; set; }
        public int TotalPlayTime { get; set;}
        public int TotalNumberOfTurn { get; set; }



        public GameStats()
        {

        }
        public GameStats(Player player, Games game)
        {
            this.Player = player;
            this.Game = game;
            this.EloRating = 420;
            this.HighestElo = 420;
            this.TotalMatches = 0;
            this.TotalWins = 0;
            this.TotalDraws = 0;
            this.TotalPlayTime = 0;
            this.TotalNumberOfTurn = 0;
        }

        public GameStats(Player player, Games game, int eloRating, int highestElo, int totalMathces, int totalWins, int totalDraws, int totalPlayTime, int totalNumberOfTurn)
        {
            this.Player = player;
            this.Game = game;
            this.EloRating = eloRating;
            this.HighestElo = highestElo;
            this.TotalMatches = totalMathces;
            this.TotalWins = totalWins;
            this.TotalDraws = totalDraws;
            this.TotalPlayTime = totalPlayTime;
            this.TotalNumberOfTurn = totalNumberOfTurn;
        }
    }
}
