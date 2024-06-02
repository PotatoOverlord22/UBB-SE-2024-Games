namespace GameWorldClassLibrary.Models
{
    public class Connect4Piece : IPiece
    {
        private int xPosition;
        private int yPosition;
        private Player player;

        public int XPosition
        {
            get { return xPosition; }
            set { xPosition = value; }
        }

        public int YPosition
        {
            get { return yPosition; }
            set { yPosition = value; }
        }

        public Player Player
        {
            get => player;
            set => player = value;
        }

        public Connect4Piece(int x, int y, Player player)
        {
            this.xPosition = x;
            this.yPosition = y;
            this.player = player;
        }
    }
}
