using GameWorldClassLibrary.Models;

namespace GameWorldClassLibrary.Services
{
    public interface IPlayService
    {
        void Play(int numberOfParameters, object[] parameters);
        bool IsGameOver();

        List<IPiece> GetBoard();

        Guid GetTurn();
        void PlayOther();
        Guid? GetWinner();

        Guid StartPlayer();
    }
}
