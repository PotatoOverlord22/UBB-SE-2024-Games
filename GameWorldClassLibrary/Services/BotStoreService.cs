using GameWorldClassLibrary.Models;
using GameWorldClassLibrary.Utils;
using TwoPlayerGames.exceptions;

namespace GameWorldClassLibrary.Services
{
    public class BotStoreService
    {
        public static IBot GetBotForTheGivenGameType(string gameType, string difficulty, Guid gameStateId, Player player, GamesContext gamesDbContext)
        {
            if (difficulty != "easy" && difficulty != "medium" && difficulty != "hard")
            {
                throw new InvalidDifficultyException();
            }

            switch (gameType)
            {
                case "Connect4":
                    return new Connect4BotService(difficulty, gameStateId, player, gamesDbContext);
                case "Chess":
                    return new Connect4BotService(difficulty, gameStateId, player, gamesDbContext);
                case "Obstruction":
                    return new Connect4BotService(difficulty, gameStateId, player, gamesDbContext);
                case "Darts":
                    return new Connect4BotService(difficulty, gameStateId, player, gamesDbContext);
                default:
                    throw new InvalidGameException();
            }
        }
    }
}
