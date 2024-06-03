using GameWorldClassLibrary.Models;
using GameWorldClassLibrary.Repositories;
using GameWorldClassLibrary.Services;
using System.Drawing;

namespace GameWorldClassLibrary.Service
{
    public class GameStateService : IGameStateService
    {
        public List<Player> Players { get; set; }
        public List<Pawn> GamePawns { get; set; }
        public List<GameTile> GameTiles { get; set; }
        public GameBoard GameBoard { get; set; }

        public static int generatedPawnIds = 0;
        public int currentPlayerIndex { get; set; }

        public delegate void PawnKilledEventHandler(object sender);
        public event PawnKilledEventHandler PawnKilled;

        public GameStateService()
        {
            Players = new List<Player>
        {
            new Player("Egg"),
            new Player("Mario"),
            new Player("Gigi"),
            new Player("Flower")
        };
            GameTiles = GenerateTiles();
            GamePawns = new List<Pawn>();
            GeneratePawns();

            GameBoard = new GameBoard(GameTiles, GamePawns, Players);
        }

        private List<Pawn> GenerateBluePawns()
        {
            List<Pawn> bluePawns = new List<Pawn>();
            // 4 pawns on tiles 0-3
            for (int index = 0; index < 4; index++)
            {
                Pawn newPawn = new Pawn(generatedPawnIds, GameTiles[index]);
                generatedPawnIds++;
                bluePawns.Add(newPawn);
            }

            return bluePawns;
        }

        public List<Pawn> GenerateYellowPawns()
        {
            List<Pawn> yellowPawns = new List<Pawn>();
            for (int index = 4; index < 8; index++)
            {
                Pawn newPawn = new Pawn(generatedPawnIds, GameTiles[index]);
                generatedPawnIds++;
                yellowPawns.Add(newPawn);
            }
            return yellowPawns;
        }

        public List<Pawn> GenerateGreenPawns()
        {
            List<Pawn> greenPawns = new List<Pawn>();
            for (int i = 8; i < 12; i++)
            {
                Pawn newPawn = new Pawn(generatedPawnIds, GameTiles[i]);
                generatedPawnIds++;
                greenPawns.Add(newPawn);
            }
            return greenPawns;
        }

        public List<Pawn> GenerateRedPawns()
        {
            List<Pawn> redPawns = new List<Pawn>();
            for (int i = 12; i < 16; i++)
            {
                Pawn newPawn = new Pawn(generatedPawnIds, GameTiles[i]);
                generatedPawnIds++;
                redPawns.Add(newPawn);
            }
            return redPawns;
        }

        public void GeneratePawns()
        {
            // associate pawns depending on the number of players
            List<string> colors = new List<string> { "Blue", "Yellow", "Green", "Red" };
            List<Pawn> bluePawns, yellowPawns, greenPawns, redPawns;
            bluePawns = new List<Pawn>();
            yellowPawns = new List<Pawn>();
            greenPawns = new List<Pawn>();
            redPawns = new List<Pawn>();

            switch (Players.Count)
            {
                case 2:
                    bluePawns = GenerateBluePawns();
                    yellowPawns = GenerateYellowPawns();
                    break;
                case 3:
                    bluePawns = GenerateBluePawns();
                    yellowPawns = GenerateYellowPawns();
                    greenPawns = GenerateGreenPawns();
                    break;
                case 4:
                    bluePawns = GenerateBluePawns();
                    yellowPawns = GenerateYellowPawns();
                    greenPawns = GenerateGreenPawns();
                    redPawns = GenerateRedPawns();
                    break;
            }
            foreach (Pawn bluePawn in bluePawns)
            {
                bluePawn.SetAssociatedPlayer(Players[0]);
                GamePawns.Add(bluePawn);
            }

            foreach (Pawn yellowPawn in yellowPawns)
            {
                yellowPawn.SetAssociatedPlayer(Players[1]);
                GamePawns.Add(yellowPawn);
            }

            if (Players.Count > 2)
            {
                foreach (Pawn greenPawn in greenPawns)
                {
                    greenPawn.SetAssociatedPlayer(Players[2]);
                    GamePawns.Add(greenPawn);
                }
            }
            if (Players.Count > 3)
            {
                foreach (Pawn redPawn in redPawns)
                {
                    redPawn.SetAssociatedPlayer(Players[3]);
                    GamePawns.Add(redPawn);
                }
            }
        }

        public List<GameTile> GenerateTiles()
        {
            List<GameTile> gameTiles =
            [
                // the blue corner
                new GameTile(0, 9, 0), // id, row, column
                new GameTile(1, 9, 1),
                new GameTile(2, 10, 0),
                new GameTile(3, 10, 1),

                // the yellow corner
                new GameTile(4, 0, 0),
                new GameTile(5, 0, 1),
                new GameTile(6, 1, 0),
                new GameTile(7, 1, 1),

                // the green corner
                new GameTile(8, 0, 9),
                new GameTile(9, 0, 10),
                new GameTile(10, 1, 9),
                new GameTile(11, 1, 10),

                // the red corner
                new GameTile(12, 9, 9),
                new GameTile(13, 9, 10),
                new GameTile(14, 10, 9),
                new GameTile(15, 10, 10),
            ];

            int index;
            int count = 16;
            for (index = 10; index >= 6; index--)
            {
                gameTiles.Add(new GameTile(count++, index, 4));
            }
            for (index = 3; index >= 0; index--)
            {
                gameTiles.Add(new GameTile(count++, 6, index));
            }
            for (index = 5; index >= 4; index--)
            {
                gameTiles.Add(new GameTile(count++, index, 0));
            }
            for (index = 1; index <= 4; index++)
            {
                gameTiles.Add(new GameTile(count++, 4, index));
            }
            for (index = 3; index >= 0; index--)
            {
                gameTiles.Add(new GameTile(count++, index, 4));
            }
            for (index = 5; index <= 6; index++)
            {
                gameTiles.Add(new GameTile(count++, 0, index));
            }
            for (index = 1; index <= 4; index++)
            {
                gameTiles.Add(new GameTile(count++, index, 6));
            }
            for (index = 7; index <= 10; index++)
            {
                gameTiles.Add(new GameTile(count++, 4, index));
            }
            for (index = 5; index <= 6; index++)
            {
                gameTiles.Add(new GameTile(count++, index, 10));
            }
            for (index = 9; index >= 6; index--)
            {
                gameTiles.Add(new GameTile(count++, 6, index));
            }
            for (index = 7; index <= 10; index++)
            {
                gameTiles.Add(new GameTile(count++, index, 6));
            }
            gameTiles.Add(new GameTile(count++, 10, 5));
            // the crosses
            // the blue cross
            for (index = 9; index >= 6; index--)
            {
                gameTiles.Add(new GameTile(count++, index, 5));
            }
            // the yellow cross
            for (index = 1; index <= 4; index++)
            {
                gameTiles.Add(new GameTile(count++, 5, index));
            }
            // the green cross
            for (index = 1; index <= 4; index++)
            {
                gameTiles.Add(new GameTile(count++, index, 5));
            }
            // the red cross
            for (index = 9; index >= 6; index--)
            {
                gameTiles.Add(new GameTile(count++, 5, index));
            }

            return gameTiles;
        }

        public int ComputeNewTileId(string pawnColor, int currentTileId, int diceValue)
        {
            // 16 first path tile
            if (currentTileId <= 3)
            {
                // blue corner
                if (diceValue == 12)
                {
                    return 16;
                }

                return diceValue + 16 - 1;
            }
            else if (currentTileId <= 7)
            {
                // yellow corner
                if (diceValue == 12)
                {
                    return 26;
                }

                return diceValue + 26 - 1;
            }
            else if (currentTileId <= 11)
            {
                if (diceValue == 12)
                {
                    return 36;
                }

                return diceValue + 36 - 1;
            }
            else if (currentTileId <= 15)
            {
                if (diceValue == 12)
                {
                    return 46;
                }
                return diceValue + 46 - 1;
            }

            // compute possible new tile
            int newTileId = currentTileId + diceValue;

            // should enter cross
            switch (pawnColor)
            {
                case "b":
                    if (currentTileId >= 56 && currentTileId <= 59)
                    {
                        if (newTileId <= 59)
                        {
                            return newTileId;
                        }
                        return currentTileId;
                    }
                    if (currentTileId <= 55 && newTileId > 55)
                    {
                        if (newTileId <= 59)
                        {
                            return newTileId;
                        }
                        else
                        {
                            return currentTileId;
                        }
                    }
                    break;
                case "y":
                    if (currentTileId >= 60 && currentTileId <= 63)
                    {
                        if (newTileId <= 63)
                        {
                            return newTileId;
                        }
                        return currentTileId;
                    }
                    if (currentTileId <= 25 && newTileId > 25)
                    {
                        if (newTileId - 26 + 60 <= 63)
                        {
                            return newTileId - 26 + 60;
                        }
                        else
                        {
                            return currentTileId;
                        }
                    }
                    break;

                case "g":
                    if (currentTileId >= 64 && currentTileId <= 67)
                    {
                        if (newTileId <= 67)
                        {
                            return newTileId;
                        }
                        return currentTileId;
                    }
                    if (currentTileId <= 35 && newTileId > 35)
                    {
                        if (newTileId - 36 + 64 <= 67)
                        {
                            return newTileId - 36 + 64;
                        }
                        else
                        {
                            return currentTileId;
                        }
                    }
                    break;

                case "r":
                    if (currentTileId >= 68 && currentTileId <= 71)
                    {
                        if (newTileId <= 71)
                        {
                            return newTileId;
                        }
                        return currentTileId;
                    }
                    if (currentTileId <= 45 && newTileId > 45)
                    {
                        if (newTileId - 46 + 68 <= 71)
                        {
                            return newTileId - 46 + 68;
                        }
                        else
                        {
                            return currentTileId;
                        }
                    }
                    break;
            }

            // Return tile only in valid range.
            return (newTileId > 55) ? (newTileId % 56) + 16 : newTileId;
        }

        public string PawnColor(Pawn pawn)
        {
            int pawnId = pawn.GetPawnId();
            if (pawnId < 4)
            {
                return "b";
            }
            if (pawnId < 8)
            {
                return "y";
            }
            if (pawnId < 12)
            {
                return "g";
            }
            return "r";
        }

        public int DetermineNextPlayerIndex()
        {
            return (currentPlayerIndex + 1) % Players.Count;
        }

        public int DetermineStartingPlayerIndex()
        {
            Random random = new Random();
            int playerIndex = random.Next(0, Players.Count - 1);

            return playerIndex;
        }

        public Pawn DeterminePawnBasedOnColumnAndRow(int column, int row)
        {
            foreach (Pawn pawn in GamePawns)
            {
                Tile occupiedTile = pawn.GetOccupiedTile();
                if (occupiedTile.GetCenterXPosition() == column && occupiedTile.GetCenterYPosition() == row)
                {
                    return pawn;
                }
            }
            throw new Exception("No pawn found");
        }

        public Tile FindEmptyHomeTileInRange(int minId, int maxId)
        {
            List<int> occupiedTiles = new List<int>();
            foreach (Pawn pawn in GamePawns)
            {
                Tile occupiedTile = pawn.GetOccupiedTile();
                occupiedTiles.Add(occupiedTile.GetTileId());
            }

            for (int index = minId; index <= maxId; index++)
            {
                if (!occupiedTiles.Contains(index))
                {
                    return GameTiles[index];
                }
            }
            throw new Exception("Can't revive pawn??");
        }

        public void KillPawn(Pawn pawn)
        {
            // current player s pawn stepped on this one so it dies
            string pawnColor = PawnColor(pawn);
            Tile newTile = null;
            switch (pawnColor)
            {
                case "b":
                    newTile = FindEmptyHomeTileInRange(0, 3);
                    break;
                case "y":
                    newTile = FindEmptyHomeTileInRange(4, 7);
                    break;
                case "g":
                    newTile = FindEmptyHomeTileInRange(8, 11);
                    break;
                default:
                    newTile = FindEmptyHomeTileInRange(12, 15);
                    break;
            }

            pawn.ChangeTile(newTile);
            GameBoard.UpdatePawns(GamePawns);

            OnPawnKilled();
        }

        public void MovePawn(Pawn pawn, int leftDiceValue, int rightDiceValue, Guid playerId)
        {
            int diceValue = leftDiceValue + rightDiceValue;
            if (diceValue == 0)
            {
                throw new Exception("Can't move pawn yet");
            }
            if (pawn.AssociatedPlayer.Id != playerId)
            {
                throw new Exception("Not your pawn :(");
            }

            int currentTileId = pawn.GetOccupiedTile().GetTileId();

            if (currentTileId < 16)
            {
                // pawn still on home tiles
                if (rightDiceValue != 6 || leftDiceValue != 6)
                {
                    throw new Exception("You have to roll two 6s!");
                }
            }

            int newTileId = ComputeNewTileId(PawnColor(pawn), currentTileId, diceValue);

            if (newTileId == currentTileId)
            {
                throw new Exception("Pawn cannot go further");
            }

            GameTile newTile = GameTiles[newTileId];

            // Eliminate pawn on the same tile if there is any
            try
            {
                Pawn enemyPawn = DeterminePawnBasedOnColumnAndRow(newTile.GetGridColumnInded(), newTile.GetGridRowIndex());
                if (enemyPawn.GetPlayer().Id != playerId)
                {
                    KillPawn(enemyPawn);
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            pawn.ChangeTile(newTile);
            GameBoard.UpdatePawns(GamePawns);
        }
        protected virtual void OnPawnKilled()
        {
            if (PawnKilled != null)
            {
                PawnKilled(this);
            }
        }

        List<Pawn> IGameStateService.GenerateBluePawns()
        {
            throw new NotImplementedException();
        }

        public string GetCurrentPlayerColor()
        {
            switch (currentPlayerIndex)
            {
                case 0: return "b";
                case 1: return "y";
                case 2: return "g";
                case 3: return "r";
                default:
                    throw new Exception("Invalid player index");
            }
        }
    }

}
