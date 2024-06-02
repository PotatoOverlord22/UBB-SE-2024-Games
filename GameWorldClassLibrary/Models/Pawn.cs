namespace GameWorldClassLibrary.Models
{
    public class Pawn
    {
        public int Id { get; set; }
        public Tile OccupiedTile { get; set; }
        public Player AssociatedPlayer { get; set; }

        public Pawn(int pawnId, Tile occupiedTile)
        {
            Id = pawnId;
            this.OccupiedTile = occupiedTile;
        }

        public Pawn(int pawnId, Tile occupiedTile, Player associatedPlayer)
        {
            Id = pawnId;
            this.OccupiedTile = occupiedTile;
            this.AssociatedPlayer = associatedPlayer;
        }

        public void ChangeTile(Tile tileToChangeTo)
        {
            OccupiedTile = tileToChangeTo;
        }
        public Tile GetOccupiedTile()
        {
            return OccupiedTile;
        }
        public int GetPawnId()
        {
            return Id;
        }
        public Player GetPlayer()
        {
            return AssociatedPlayer;
        }

        public void SetAssociatedPlayer(Player associatedPlayer)
        {
            this.AssociatedPlayer = associatedPlayer;
        }

        public Player GetAssociatedPlayer()
        {
            return AssociatedPlayer;
        }
    }
}