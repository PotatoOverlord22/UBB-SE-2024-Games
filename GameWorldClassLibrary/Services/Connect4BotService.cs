using GameWorldClassLibrary.Models;
using GameWorldClassLibrary.Repositories;
using GameWorldClassLibrary.Utils;

namespace GameWorldClassLibrary.Services
{
    public class Connect4BotService(string difficulty, Guid gameStateId, Player player, GamesContext gameDbContext) : IBot
    {
        private readonly Connect4Service gameService = new(gameStateId, player, Player.Null(), new Connect4Repository(gameDbContext));
        private readonly Player player = player;
        private readonly int searchDepth = difficulty.ToLower() switch
        {
            "easy" => 1,
            "medium" => 3,
            "hard" => 5,
            _ => 1,
        };

        public (int nrParameters, object[] parameters) Play(IGame game)
        {
            throw new NotImplementedException();
        }

        public static int Score_Connect_4_Block(List<IPiece> connect4Block, Player currentPlayer, Player currentOpponent)
        {
            int score = 0;
            int playerPieces = 0;
            int opponentPieces = 0;
            foreach (IPiece piece in connect4Block)
            {
                if (piece.Player == currentPlayer)
                {
                    playerPieces++;
                }
                else if (piece.Player != currentOpponent)
                {
                    opponentPieces++;
                }
            }
            if (opponentPieces == 4)
            {
                score += 100;
            }
            else if (opponentPieces == 3 && playerPieces == 1)
            {
                score += 5;
            }
            else if (opponentPieces == 2 && playerPieces == 2)
            {
                score += 2;
            }

            if (playerPieces == 3 && opponentPieces == 1)
            {
                score -= 4;
            }

            return score;
        }

        private List<IPiece> GetCentreColumn()
        {
            List<IPiece> centreColumn = new();
            foreach (IPiece piece in gameService.GetGame().Board.Board)
            {
                if (piece.XPosition == 3)
                {
                    centreColumn.Add(piece);
                }
            }
            return centreColumn;
        }

        private int ScoreCenterColumn(List<IPiece> centreColumn)
        {
            int score = 0;
            foreach (IPiece piece in centreColumn)
            {
                if (piece.Player != player)
                {
                    score += 3;
                }
            }
            return score;
        }

        private int ScoreBoard(Player currentPlayer, Player currentOpponent)
        {
            int score = 0;
            List<IPiece> centre_column = GetCentreColumn();
            score += ScoreCenterColumn(centre_column);

            for (int row = 0; row < Constants.BOARD_WIDTH - 4; row++)
            {
                for (int column = 0; column < Constants.BOARD_LENGTH; column++)
                {
                    List<IPiece> connect4Block = new(4);
                    for (int k = 0; k < 4; k++)
                    {
                        connect4Block[k] = gameService.GetGame().Board.GetPiece(row + k, column);
                    }
                    score += Score_Connect_4_Block(connect4Block, currentPlayer, currentOpponent);
                }
            }

            for (int row = 0; row < Constants.BOARD_WIDTH; row++)
            {
                for (int column = 0; column < Constants.BOARD_LENGTH - 4; column++)
                {
                    List<IPiece> connect4Block = new(4);
                    for (int k = 0; k < 4; k++)
                    {
                        connect4Block[k] = gameService.GetGame().Board.GetPiece(row, column + k);
                    }
                    score += Score_Connect_4_Block(connect4Block, currentPlayer, currentOpponent);
                }
            }

            for (int row = 0; row < Constants.BOARD_WIDTH - 4; row++)
            {
                for (int column = 0; column < Constants.BOARD_LENGTH - 4; column++)
                {
                    List<IPiece> connect4Block = new(4);
                    for (int k = 0; k < 4; k++)
                    {
                        connect4Block[k] = gameService.GetGame().Board.GetPiece(row + k, column + k);
                    }
                    score += Score_Connect_4_Block(connect4Block, currentPlayer, currentOpponent);
                }
            }

            for (int row = 0; row < Constants.BOARD_WIDTH - 4; row++)
            {
                for (int column = 0; column < Constants.BOARD_LENGTH - 4; column++)
                {
                    List<IPiece> connect4Block = new(4);
                    for (int k = 0; k < 4; k++)
                    {
                        connect4Block[k] = gameService.GetGame().Board.GetPiece(row + 3 - k, column + k);
                    }
                    score += Score_Connect_4_Block(connect4Block, currentPlayer, currentOpponent);
                }
            }

            return score;
        }
        private bool IsTerminalNode() => gameService.CheckCurrentState() >= 0;

        public (int, int) GetBestMove(int? searchDepth = null, bool isMaximizingPlayer = true)
        {
            searchDepth ??= this.searchDepth;
            if (searchDepth == 0 || IsTerminalNode())
            {
                if (searchDepth == 0)
                {
                    Player? currentOpponent;
                    Player? currentPlayer;
                    if (isMaximizingPlayer)
                    {
                        currentPlayer = Player.Null();
                        currentOpponent = player;
                    }
                    else
                    {
                        currentOpponent = Player.Null();
                        currentPlayer = player;
                    }
                    return (-1, ScoreBoard(currentPlayer, currentOpponent));
                }
                else
                {
                    if (gameService.GetGame().GameState.Winner == Player.Null())
                    {
                        return (-1, int.MaxValue);
                    }
                    else if (gameService.GetGame().GameState.Winner == player)
                    {
                        return (-1, int.MinValue);
                    }
                    else
                    {
                        return (-1, 0);
                    }
                }
            }
            else
            {
                Random random = new();
                if (isMaximizingPlayer)
                {
                    int optimalScore = int.MinValue;
                    int optimalColumn = -1;
                    int check = 0;
                    HashSet<int> ints = new();
                    while (check < 7)
                    {
                        int randInt = random.Next(0, 7);
                        if (ints.Contains(randInt))
                        {
                            if (gameService.CheckValidity(randInt))
                            {
                                IPiece piece = gameService.DropPiece(randInt, Player.Null());
                                int score = GetBestMove(searchDepth - 1, false).Item2;
                                ((Connect4Board)gameService.GetGame().Board).RemovePiece(piece.XPosition, piece.YPosition);
                                if (score > optimalScore)
                                {
                                    optimalScore = score;
                                    optimalColumn = randInt;
                                }
                            }
                            check++;
                        }
                        ints.Add(randInt);
                    }
                    return (optimalColumn, optimalScore);
                }
                else
                {
                    int optimalScore = int.MaxValue;
                    int optimalColumn = -1;
                    int check = 0;
                    HashSet<int> ints = new();
                    while (check < 7)
                    {
                        int randInt = random.Next(0, 7);
                        if (ints.Contains(randInt))
                        {
                            if (gameService.CheckValidity(randInt))
                            {
                                IPiece piece = gameService.DropPiece(randInt, player);
                                int score = GetBestMove(searchDepth - 1, true).Item2;
                                ((Connect4Board)gameService.GetGame().Board).RemovePiece(piece.XPosition, piece.YPosition);
                                if (score < optimalScore)
                                {
                                    optimalScore = score;
                                    optimalColumn = randInt;
                                }
                            }
                            check++;
                        }
                        ints.Add(randInt);
                    }
                    return (optimalColumn, optimalScore);
                }
            }
            // int bestMove = -1;
            // int bestScore = isMaximizingPlayer ? int.MinValue : int.MaxValue;
            // if (searchDepth == 0 || IsTerminalNode())
            // {
            //    return ScoreBoard();
            // }
            // for (int i = 0; i < Constants.BOARD_WIDTH; i++)
            // {
            //    if (gameService.GetGame().Board.GetPiece(i, 0) == null)
            //    {
            //        gameService.Play(1, new object[] { i });
            //        int score = GetBestMove(searchDepth - 1, !isMaximizingPlayer);
            //        //gameService.GetGame().Board.removePiece(i, 0);
            //        //TODO: UNDO MOVE
            //        if (isMaximizingPlayer)
            //        {
            //            if (score > bestScore)
            //            {
            //                bestScore = score;
            //                bestMove = i;
            //            }
            //        }
            //        else
            //        {
            //            if (score < bestScore)
            //            {
            //                bestScore = score;
            //                bestMove = i;
            //            }
            //        }
            //    }
            // }
        }
    }
}
