using GameWorldClassLibrary.Models;
using GameWorldClassLibrary.Utils;

namespace GameWorldClassLibrary.Repositories
{
    public class Connect4RepositoryDB : BaseGameRepositoryDB, IGameRepo
    {
        private readonly GamesContext context;
        public Connect4RepositoryDB(GamesContext gamesDbContext) : base(gamesDbContext)
        {
            context = gamesDbContext;
        }

        public override Dictionary<Guid, IGame> GetGames()
        {
            Dictionary<Guid, IGame> games = new Dictionary<Guid, IGame>();
            Guid GameId = context.Games.Find("Connect4").Id;
            GameState gameState = context.GameStates.Find(GameId);

            IGame connect4Game = LoadGameFromUnfinishedState(gameState);
            games.Add(gameState.Id, connect4Game);
            return games;
        }
    }
}
