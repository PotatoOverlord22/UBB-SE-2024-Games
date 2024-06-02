using GameWorldClassLibrary.Models;
using GameWorldClassLibrary.Utils;

namespace GameWorldClassLibrary.Repositories
{
    public class ObstructionRepository : BaseGameRepository, IGameRepo
    {
        private readonly GamesContext context;
        public ObstructionRepository(GamesContext gamesDbContext) : base(gamesDbContext)
        {
            context = gamesDbContext;
        }

        public override Dictionary<Guid, IGame> GetGames()
        {
            Dictionary<Guid, IGame> games = new Dictionary<Guid, IGame>();
            Guid GameId = context.Games.Find("Obstruction").Id;
            GameState gameState = context.GameStates.Find(GameId);

            IGame obstructionGame = LoadGameFromUnfinishedState(gameState);
            games.Add(gameState.Id, obstructionGame);
            return games;
        }
    }
}
