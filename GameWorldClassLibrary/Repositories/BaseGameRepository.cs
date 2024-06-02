using GameWorldClassLibrary.Models;
using GameWorldClassLibrary.Services;
using GameWorldClassLibrary.Utils;
using Microsoft.EntityFrameworkCore;
using TwoPlayerGames.exceptions;

namespace GameWorldClassLibrary.Repositories
{
    public abstract class BaseGameRepository : IGameRepo
    {
        private readonly GamesContext context;
        private Dictionary<Guid, IGame> games;

        public BaseGameRepository(GamesContext gamesDbContext)
        {
            context = gamesDbContext;
            games = GetGames();
        }

        public void UpdateRepo()
        {
            games = GetGames();
        }
        public abstract Dictionary<Guid, IGame> GetGames();

        public IGame? GetGameById(Guid id)
        {
            UpdateRepo();
            if (games != null)
            {
                try
                {
                    return games[id];
                }
                catch (KeyNotFoundException)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public void AddGame(IGame game)
        {
            games.Add(game.GameState.Id, game);
            context.GameStates.Add(game.GameState);
        }

        public void UpdateGame(IGame game)
        {
            context.GameStates.Update(game.GameState); 
        }

        public void RemoveGame(Guid id)
        {
            games.Remove(id);
        }

        public IGame LoadGameFromUnfinishedState(GameState unifinishedGameState)
        {
            IGame game = unifinishedGameState.GameType.Name switch
            {
                "Obstruction" => new ObstructionGameService(unifinishedGameState),
                "Connect4" => new Connect4GameService(unifinishedGameState),
                _ => throw new GameTypeNotFoundException("Game type not found")
            };
            return game;
        }

        public IGame GetGameFromDatabase(Guid id)
        {
            var gameState = context.GameStates
                .Include(gs => gs.Players[0])
                .Include(gs => gs.Players[1])
                .Include(gs => gs.GameType)
                .FirstOrDefault(gs => gs.Id == id);

            if (gameState == null)
            {
                return null;
            }

            IGame game = gameState.GameType.Name switch
            {
                "Obstruction" => new ObstructionGameService(gameState),
                "Connect4" => new Connect4GameService(gameState),
                _ => throw new GameTypeNotFoundException("Game type not found")
            };

            return game;
        }
    }
}
