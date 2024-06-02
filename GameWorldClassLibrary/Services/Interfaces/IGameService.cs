using GameWorldClassLibrary.Models;
using TwoPlayerGames;

namespace GameWorldClassLibrary.Services
{
    public interface IGameService
    {
        IGame Play(int nrParameters, object[] parameters);

        IGame GetGame();

        void SetGame(IGame game);
    }
}
