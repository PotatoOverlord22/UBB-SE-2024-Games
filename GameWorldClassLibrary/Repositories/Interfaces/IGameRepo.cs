using GameWorldClassLibrary.Models;

namespace GameWorldClassLibrary.Repositories
{
    public interface IGameRepo
    {
        Dictionary<Guid, IGame> GetGames();
        IGame? GetGameById(Guid id);
        void AddGame(IGame game);
        void UpdateGame(IGame game);
        void RemoveGame(Guid id);
        IGame LoadGameFromUnfinishedState(GameState unfinishedGameState);
        IGame GetGameFromDatabase(Guid id);
    }
}
