using GameWorldClassLibrary.Models;
using GameWorldClassLibrary.Repositories;
using GameWorldClassLibrary.Utils;
using TwoPlayerGames.exceptions;

namespace GameWorldClassLibrary.Services
{
    public class Connect4Service : IGameService
    {
        private Connect4GameService connect4Game;
        private readonly List<Player> players;
        private IGameRepo connect4Repo;

        public Connect4Service(Guid gameStateID, Player player1, Player player2, IGameRepo repo)
        {
            connect4Repo = repo;
            players = [player1, player2];

            if (gameStateID == Guid.Empty)
            {
                connect4Game = new Connect4GameService(player1, player2);
                if (player2.Name != "Bot")
                {
                    connect4Repo.AddGame(connect4Game);
                }
                int turn = 0;
                connect4Game.GameState.Turn = turn;
            }
            else
            {
                connect4Game = (Connect4GameService?)connect4Repo.GetGameById(gameStateID);
            }
        }

        /// <summary>
        /// Gets the next valid row for a piece to be placed in a column
        /// </summary>
        /// <param name="column">The column we need check for first valid row</param>
        /// <returns>The first valid row of that column</returns>
        public int GetNextValidRow(int column)
        {
            for (int i = Constants.BOARD_WIDTH - 1; i >= 0; i--)
            {
                if (connect4Game.Board.GetPiece(column, i).Player.Name == "Null")
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// Checks if the column is valid
        /// </summary>
        /// <param name="column">The column we need check for first valid row</param>
        /// <returns>true if the column is valid, false othwerise</returns>
        public bool CheckValidity(int column)
        {
            List<int> availableColumns = new List<int>();
            Connect4Board board = (Connect4Board)connect4Game.Board;
            for (int i = 0; i < Constants.BOARD_LENGTH; i++)
            {
                if (board.GetPiece(i, Constants.LAST_LOCATION).Player.Name == "Null")
                {
                    availableColumns.Add(i);
                }
            }
            return availableColumns.Contains(column);
        }

        private IPiece DropPiece(int column)
        {
            Connect4Board board = (Connect4Board)connect4Game.Board;
            if (CheckValidity(column) == false)
            {
                throw new InvalidColumnException();
            }
            int row = GetNextValidRow(column);

            IPiece piece = new Connect4Piece(column, row, GetCurrentPlayer());
            board.AddPiece(piece);
            return piece;
            // _inGameService.Play(sound);
        }

        public IPiece DropPiece(int column, Player player)
        {
            Connect4Board board = (Connect4Board)connect4Game.Board;
            if (CheckValidity(column) == false)
            {
                throw new InvalidColumnException();
            }
            int row = GetNextValidRow(column);

            IPiece piece = new Connect4Piece(column, row, player);
            board.AddPiece(piece);
            return piece;
            // _inGameService.Play(sound);
        }

        /// <summary>
        /// Checks if the connect4Block is a connect 4 block
        /// </summary>
        /// <param name="connect4Block">A 4 pieces length list</param>
        /// <returns>The id of the player who has connect 4 or null otherwise</returns>
        private bool IsWinningState(List<IPiece> connect4Block)
        {
            int player1PieceCount = 0;
            int player2PieceCount = 0;
            for (int i = 0; i < 4; i++)
            {
                if (connect4Block[i].Player.Name == "Null")
                {
                    return false;
                }
                if (connect4Block[i].Player.Id == players[0].Id)
                {
                    player1PieceCount++;
                }
                else
                {
                    player2PieceCount++;
                }
            }
            if (player1PieceCount == Constants.WINNING_LENGTH || player2PieceCount == Constants.WINNING_LENGTH)
            {
                return true;
            }

            return false;
        }

        public int CheckCurrentState()
        {
            Connect4Board board = (Connect4Board)connect4Game.Board;

            if (board.IsBoardFull())
            {
                return 1;
            }

            int rows = 6;
            int cols = 7;

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col <= cols - 4; col++)
                {
                    if (connect4Game.Board.GetPiece(row, col).Player.Id == players[0].Id &&
                         connect4Game.Board.GetPiece(row, col + 1).Player.Id == players[0].Id &&
                         connect4Game.Board.GetPiece(row, col + 2).Player.Id == players[0].Id &&
                         connect4Game.Board.GetPiece(row, col + 3).Player.Id == players[0].Id)
                    {
                        return 0;
                    }
                }
            }

            for (int col = 0; col < cols; col++)
            {
                for (int row = 0; row <= rows - 4; row++)
                {
                    if (connect4Game.Board.GetPiece(row, col).Player.Id == players[0].Id &&
                        connect4Game.Board.GetPiece(row + 1, col).Player.Id == players[0].Id &&
                        connect4Game.Board.GetPiece(row + 2, col).Player.Id == players[0].Id &&
                        connect4Game.Board.GetPiece(row + 3, col).Player.Id == players[0].Id)
                    {
                        return 0;
                    }
                }
            }

            // Check diagonally (down-right)
            for (int row = 0; row <= rows - 4; row++)
            {
                for (int col = 0; col <= cols - 4; col++)
                {
                    if (connect4Game.Board.GetPiece(row, col).Player.Id == players[0].Id &&
                        connect4Game.Board.GetPiece(row + 1, col + 1).Player.Id == players[0].Id &&
                        connect4Game.Board.GetPiece(row + 2, col + 2).Player.Id == players[0].Id &&
                        connect4Game.Board.GetPiece(row + 3, col).Player.Id == players[0].Id)
                    {
                        return 0;
                    }
                }
            }

            // Check diagonally (up-right)
            for (int row = 3; row < rows; row++)
            {
                for (int col = 0; col <= cols - 4; col++)
                {
                    if (connect4Game.Board.GetPiece(row, col).Player.Id == players[0].Id &&
                        connect4Game.Board.GetPiece(row - 1, col + 1).Player.Id == players[0].Id &&
                        connect4Game.Board.GetPiece(row - 2, col + 2).Player.Id == players[0].Id &&
                        connect4Game.Board.GetPiece(row - 3, col + 3).Player.Id == players[0].Id)
                    {
                        return 0;
                    }
                }
            }

            return -1;
        }

        private void SwitchTurn()
        {
            connect4Game.CurrentPlayerIndex = (connect4Game.CurrentPlayerIndex + 1) % 2;
        }

        public IGame Play(int nrParameters, object[] parameters)
        {
            Player player = GetCurrentPlayer();

            int column = Convert.ToInt32(parameters[0]);
            DropPiece(column);
            int winner = CheckCurrentState();
            if (winner == 0)
            {
                connect4Game.GameState.Winner = player;
            }

            SwitchTurn();

            connect4Game.SaveGame();
            connect4Repo.UpdateGame(connect4Game);

            return connect4Game;
        }

        private Player GetCurrentPlayer()
        {
            return players[connect4Game.GameState.Turn];
        }

        public IGame GetGame()
        {
            return connect4Game;
        }

        public void SetGame(IGame game)
        {
            connect4Game = (Connect4GameService)game;
        }
    }
}
