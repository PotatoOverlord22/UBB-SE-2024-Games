namespace GameWorldClassLibrary.Models
{
    public class GameBoard : Games
    {
        private List<Player> players;
        private List<Pawn> gamePawns;
        private List<GameTile> gameTiles;
        private Dice sixSidedDice;

        public GameBoard(List<GameTile> tiles, List<Pawn> pawns, List<Player> players)
        {
            gameTiles = tiles;
            this.players = players;
            sixSidedDice = new Dice();
            gamePawns = pawns;
        }

        public List<Player> GetPlayers()
        {
            return players;
        }

        public List<GameTile> GetTiles()
        {
            return gameTiles;
        }

        public List<Pawn> GetPawns()
        {
            return gamePawns;
        }

        public Dice GetDice()
        {
            return sixSidedDice;
        }

        public void UpdatePawns(List<Pawn> newPawns)
        {
            gamePawns = newPawns;
        }
    }
}
