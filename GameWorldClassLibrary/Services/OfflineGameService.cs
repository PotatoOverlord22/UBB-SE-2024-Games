using GameWorldClassLibrary.Models;
using GameWorldClassLibrary.Repositories;
using GameWorldClassLibrary.Utils;
using TwoPlayerGames.exceptions;

namespace GameWorldClassLibrary.Services
{
    public class OfflineGameService : IPlayService
    {
        private IGameService gameService;
        private IPlayService statsService;
        private string gameType;
        private Guid startPlayer;
        private IBot bot;
        private Player player;

        public OfflineGameService(string gameType, Player player, GamesContext gamesDbContext, List<object> optional_params)
        {
            this.gameType = gameType;
            this.player = player;
            Random random = new Random();
            // startPlayer = random.Next(0, 2) == 0 ? player.Id : Guid.Empty;
            startPlayer = player.Id;
            optional_params.Add("easy");
            optional_params.Add("BULLET");
            string difficulty = (string)optional_params[0];
            switch (gameType)
            {
                case "Obstruction":
                    int boardWidth = (int)optional_params[1];
                    int boardHeight = (int)optional_params[2];
                    gameService = new ObstructionService(Guid.Empty, player, Player.Bot(), boardWidth, boardHeight, new ObstructionRepository(gamesDbContext));
                    break;
                case "Connect4":

                    gameService = new Connect4Service(Guid.Empty, player, Player.Bot(), new Connect4Repository(gamesDbContext));
                    break;
            }
            bot = BotStoreService.GetBotForTheGivenGameType(gameType, difficulty, gameService.GetGame().GameState.Id, player, gamesDbContext);
        }

        public OfflineGameService()
        {
        }

        public void Play(int nrParameters, object[] parameters)
        {
            if (GetTurn() != player.Id)
            {
                throw new NotYourTurnException();
            }
            gameService.Play(nrParameters, parameters);
            if (IsGameOver())
            {
                // statsService.UpdateStats;
            }
        }

        public bool IsGameOver()
        {
            if (gameService.GetGame().GameState.Winner != null)
            {
                // statsService.UpdateStats;
                return true;
            }
            return false;
        }

        public List<IPiece> GetBoard()
        {
            return gameService.GetGame().Board.Board;
        }

        public Guid GetTurn()
        {
            return gameService.GetGame().CurrentPlayer.Id;
        }

        public void PlayOther()
        {
            Thread.Sleep(3000);
            if (gameType == "Connect4")
            {
                int column = ((Connect4BotService)bot).GetBestMove().Item1;
                object[] list = { column };
                gameService.Play(1, list);
            }
            if (gameType == "Darts")
            {
                // int xTarget = ((DartsBot)bot).GetBestMove();
                // int yTarget = ((DartsBot)bot).GetBestMove();
                // int accuracy = 50;
                // object[] list = { xTarget, yTarget, accuracy };
                // gameService.Play(1, list);
            }
        }

        public Guid? GetWinner()
        {
            return gameService.GetGame().GameState.Winner.Id;
        }

        public Guid StartPlayer()
        {
            return startPlayer;
        }
    }
}
