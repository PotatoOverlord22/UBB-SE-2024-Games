namespace GameWorldClassLibrary.Models
{
    public class GameTile : Tile
    {
        private int tileId;
        private int gridRowIndex;
        private int gridColumnIndex;

        // someone find a solution
        public GameTile(int tileId, int gridRowIndex, int gridColumnIndex) : base(tileId, gridColumnIndex, gridRowIndex)
        {
            this.tileId = tileId;
            this.gridRowIndex = gridRowIndex;
            this.gridColumnIndex = gridColumnIndex;
        }

        public int GetTileId()
        {
            return tileId;
        }

        public int GetGridRowIndex()
        {
            return gridRowIndex;
        }

        public int GetGridColumnInded()
        {
            return gridColumnIndex;
        }
    }
}
