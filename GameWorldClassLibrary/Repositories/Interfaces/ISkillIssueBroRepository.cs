using GameWorldClassLibrary.Models;

namespace GameWorldClassLibrary.Repositories
{
    public interface ISkillIssueBroRepository
    {
        Task<string> GetCurrentPlayerColor();
        Task<List<Pawn>> GetPawns();
        Task MovePawnBasedOnClick(int column, int row, int leftDiceValue, int rightDiceValue);
        Task NextPlayer();
        Task<int> RollDice();
    }
}
