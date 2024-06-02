using System;

namespace GameWorldClassLibrary.Models
{
    public class GameState
    {
        private Guid id;
        private Player[] players;
        private Games gameType;
        private Player? winnerPlayer;
        private string stateJson;
        private int turn;
        private int timePlayed;

        public Guid Id { get => id; set => id = value; }
        public Player[] Players { get => players; set => players = value; }

        public Games GameType { get => gameType; }
        public string StateJson { get => stateJson; set => stateJson = value; }

        public Player? Winner { get => winnerPlayer; set => winnerPlayer = value; }
        public int Turn { get => turn; set => turn = value; }

        public int TimePlayed { get => timePlayed; set => timePlayed = value; }

        public GameState(Player player1, Player player2, Games gameType)
        {
            this.id = Guid.NewGuid();
            this.players = new Player[2];
            this.players[0] = player1;
            this.players[1] = player2;
            this.gameType = gameType;
            this.winnerPlayer = null;
            this.turn = 0;
            this.timePlayed = 0;
            this.stateJson = string.Empty;
        }

        public GameState(Player player1, Player player2, Games gameType, int turn)
        {
            this.id = Guid.NewGuid();
            this.players = new Player[2];
            this.players[0] = player1;
            this.players[1] = player2;
            this.gameType = gameType;
            this.winnerPlayer = null;
            this.turn = turn;
            this.timePlayed = 0;
            this.stateJson = string.Empty;
        }

        public GameState()
        {
            this.id = Guid.Empty;
            this.players = new Player[2];
            this.players[0] = new Player();
            this.players[1] = new Player();
            this.gameType = new Games();
            this.winnerPlayer = null;
            this.turn = 0;
            this.timePlayed = 0;
            this.stateJson = string.Empty;
        }

        public GameState(Guid gameStateId, Player player1, Player player2, Games gameType, int turn, int timePlayed, Player? winner, string jsonString)
        {
            this.id = gameStateId;
            this.players = new Player[2];
            this.players[0] = player1;
            this.players[1] = player2;
            this.gameType = gameType;
            this.winnerPlayer = winner;
            this.turn = turn;
            this.timePlayed = timePlayed;
            this.stateJson = jsonString;
        }
    }
}