using System;
using System.Collections.Generic;
using System.Linq;
using TwoPlayerGames;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GameWorldClassLibrary.Models
{
    public class ObstructionBoard : IBoard
    {
        private List<IPiece> obstractionPieces;
        private int width;
        private int height;

        [JsonIgnore]
        public List<IPiece> Board { get => obstractionPieces; set => obstractionPieces = value; }

        [JsonProperty]
        public List<ObstructionPiece> JsonBoard
        {
            get
            {
                List<ObstructionPiece> obstructionPieces = new List<ObstructionPiece>();
                foreach (IPiece piece in this.obstractionPieces)
                {
                    obstructionPieces.Add((ObstructionPiece)piece);
                }
                return obstructionPieces;
            }
        }
        public int GetWidth { get => width; set => width = value; }
        public int GetHeight { get => height; set => height = value; }

        public ObstructionBoard(int width, int height)
        {
            this.obstractionPieces = new List<IPiece>();
            this.width = width;
            this.height = height;
        }

        public ObstructionBoard()
        {
            this.obstractionPieces = new List<IPiece>();
        }
        public IPiece? GetPiece(int xPosition, int yPosition)
        {
            return this.obstractionPieces.FirstOrDefault(piece => piece.XPosition == xPosition && piece.YPosition == yPosition);
        }
        public void AddPiece(IPiece piece)
        {
            this.obstractionPieces.Add(piece);
        }
        public bool IsFull() => this.width * this.height == this.obstractionPieces.Count;

        public void GetFromJObject(JObject obj)
        {
            throw new NotImplementedException();
        }

        public void GetFromJToken(JToken jsonObject)
        {
            List<IPiece> obstructionPieces = new List<IPiece>();
            foreach (JToken token in jsonObject["JsonBoard"])
            {
                ObstructionPiece obstructionPiece = token.ToObject<ObstructionPiece>();
                obstructionPieces.Add(obstructionPiece);
            }
            this.Board = obstructionPieces;
            this.GetWidth = jsonObject["Width"].ToObject<int>();
            this.GetHeight = jsonObject["Height"].ToObject<int>();
        }
    }
}