namespace GameWorldClassLibrary.Models
{
    public class Tile
    {
        public int Id { get; set; }
        public float CenterPositionX { get; set; }
        public float CenterPositionY { get; set; }


        public Tile()
        {
        }
        public Tile(int tileId, float centerXPosition, float centerYPosition)
        {
            Id = tileId;
            CenterPositionX = centerXPosition;
            CenterPositionY = centerYPosition;
        }
        public float GetCenterXPosition()
        {
            return CenterPositionX;
        }
        public float GetCenterYPosition()
        {
            return CenterPositionY;
        }
        public int GetTileId()
        {
            return Id;
        }
    }
}
