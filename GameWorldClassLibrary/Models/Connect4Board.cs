using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GameWorldClassLibrary.Models
{
    public class Connect4Board : IBoard
    {
        private List<IPiece> connect4Pieces;
        private int boardWidth;
        private int boardHeight;

        [JsonIgnore]
        public List<IPiece> Board { get => connect4Pieces; set => connect4Pieces = value; }

        [JsonProperty]
        private List<Connect4Piece> JsonBoard
        {
            get
            {
                List<Connect4Piece> connect4Pieces = new List<Connect4Piece>();
                foreach (IPiece bolt in this.connect4Pieces)
                {
                    connect4Pieces.Add((Connect4Piece)bolt);
                }
                return connect4Pieces;
            }
        }

        public int GetWidth { get => boardWidth; set => boardWidth = value; }
        public int GetHeight { get => boardHeight; set => boardHeight = value; }

        public Connect4Board()
        {
            this.connect4Pieces = new List<IPiece>();
            this.boardWidth = 8;
            this.boardHeight = 8;
        }

        public IPiece? GetPiece(int xPosition, int yPosition)
        {
            IPiece piece = this.connect4Pieces.FirstOrDefault(piece => piece.XPosition == xPosition && piece.YPosition == yPosition);
            if (piece == null)
            {
                return new Connect4Piece(xPosition, yPosition, Player.Null());
            }

            return piece;
        }

        public void AddPiece(IPiece piece)
        {
            this.connect4Pieces.Add(piece);
        }

        public bool IsBoardFull() => this.boardWidth * this.boardHeight == this.connect4Pieces.Count;

        public void RemovePiece(int xPosition, int yPosition)
        {
            this.connect4Pieces.Remove(this.GetPiece(xPosition, yPosition));
        }

        public void GetFromJToken(JToken jsonObject)
        {
            List<IPiece> connect4Pieces = new List<IPiece>();
            foreach (JToken token in jsonObject["JsonBoard"])
            {
                Connect4Piece connect4Piece = token.ToObject<Connect4Piece>();
                connect4Pieces.Add(connect4Piece);
            }
            this.Board = connect4Pieces;
        }
    }
}

