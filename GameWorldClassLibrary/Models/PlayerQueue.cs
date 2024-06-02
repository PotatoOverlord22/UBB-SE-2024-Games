using System.ComponentModel.DataAnnotations.Schema;

namespace GameWorldClassLibrary.Models
{
    public class PlayerQueue
    {
        public Guid Id { get; set; }
        public Guid PlayerId { get; set; }
        [ForeignKey("PlayerId")]
        private Player player;
        public Guid GameId { get; set; }
        [ForeignKey("GameId")]
        private Games gameType;
        private int eloRating;
        private int? obstructionWidth;
        private int? obstructionHeight;

        public Player Player { get => player; set => player = value; }
        public Games GameType { get => gameType; set => gameType = value; }
        public int EloRating { get => eloRating; set => eloRating = value; }
        public int? ObstractionWidth { get => obstructionWidth; set => obstructionWidth = value; }
        public int? ObstractionHeight { get => obstructionHeight; set => obstructionHeight = value; }

        public PlayerQueue(Player player, Games gameType, int elo, int? obstructionWidth, int? obstructionHeigth)
        {
            this.player = player;
            this.gameType = gameType;
            this.eloRating = elo;
            this.obstructionWidth = obstructionWidth;
            this.obstructionHeight = obstructionHeigth;
        }

        public PlayerQueue()
        {
            this.player = new Player();
            this.gameType = new Games();
            this.eloRating = 0;
            this.obstructionWidth = null;
            this.obstructionHeight = null;
        }
    }
}
