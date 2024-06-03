using GameWorldClassLibrary.Models;
using GameWorldClassLibrary.Service;

namespace GameWorldClassLibrary.Services
{
    public interface IGameStateService
    {
        List<Player> Players { get; set; }
        List<Pawn> GamePawns { get; set; }
        List<GameTile> GameTiles { get; set; }
        GameBoard GameBoard { get; set; }
        int currentPlayerIndex { get; set; }

        event GameStateService.PawnKilledEventHandler PawnKilled;

        List<Pawn> GenerateBluePawns();
        List<Pawn> GenerateYellowPawns();
        List<Pawn> GenerateGreenPawns();
        List<Pawn> GenerateRedPawns();
        void GeneratePawns();
        List<GameTile> GenerateTiles();
        int ComputeNewTileId(string pawnColor, int currentTileId, int diceValue);
        string PawnColor(Pawn pawnId);
        int DetermineNextPlayerIndex();
        int DetermineStartingPlayerIndex();
        Pawn DeterminePawnBasedOnColumnAndRow(int column, int row);
        Tile FindEmptyHomeTileInRange(int minId, int maxId);
        void KillPawn(Pawn pawnId);
        void MovePawn(Pawn pawn, int leftDiceValue, int rightDiceValue, Guid playerId);
        string GetCurrentPlayerColor();
    }
}
