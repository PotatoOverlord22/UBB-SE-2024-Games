using GameWorldClassLibrary.Models;

namespace GameWorldClassLibrary.Services
{
    public interface ISkillIssueBroService
    {
        Task <string> GetCurrentPlayerColor();
        Task<List<Pawn>> GetPawns();
        void MovePawnBasedOnClick(int column, int row, int leftDiceValue, int rightDiceValue);
        void NextPlayer();
        Task<int> RollDice();
    }
}
