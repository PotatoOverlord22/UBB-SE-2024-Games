using GameWorldClassLibrary.Models;

namespace GameWorldClassLibrary.Services
{
    public interface ISkillIssueBroService
    {
        Task <string> GetCurrentPlayerColor();
        Task<List<Pawn>> GetPawns();
        Task MovePawnBasedOnClick(int column, int row, int leftDiceValue, int rightDiceValue);
        Task NextPlayer();
        Task<int> RollDice();
    }
}
