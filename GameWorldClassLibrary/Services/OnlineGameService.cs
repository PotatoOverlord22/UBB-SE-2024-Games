using GameWorldClassLibrary.Models;
using GameWorldClassLibrary.Repositories;
using GameWorldClassLibrary.Service;
using GameWorldClassLibrary.Utils;
using System.Net;
using System.Net.Sockets;
using System.Text;
using TwoPlayerGames.exceptions;

namespace GameWorldClassLibrary.Services
{
    public class OnlineGameService : IPlayService
    {
        private IGameService gameService;
        private IPlayService statsService;
        private BaseGameRepositoryDB gameRepo;
        private Guid startPlayer;

        private IPlayerRepository playerRepository;

        private TcpListener? listener = null;
        private TcpClient? client = null;
        private Socket? socket = null;

        private bool host = false;
        private bool firstTurn = true;

        private int elo;
        private int boardWidth;
        private int boardHeight;
        private PlayerQueue playerQueue;
        private Player opponentPlayer;

        private Player player;

        private string gameType;

        public void SetFirstTurn()
        {
            firstTurn = false;
        }
        public OnlineGameService(string gameType, Player player, GamesContext gameDbContext, IPlayerRepository playerRepository, params object[] optional_params)
        {
            this.playerRepository = playerRepository;
            this.player = player;
            this.gameType = gameType;
            Games game = GameStore.Games[gameType];
            // TODO: get elo from statsService
            elo = 500;
            boardWidth = 0;
            boardHeight = 0;
            PlayerQueue playerQueue;
            Player opponentPlayer;
            if (optional_params.Length == 1)
            {
                string mode = (string)optional_params[0];
                playerQueue = new PlayerQueue(player, game, elo, null, null);
            }
            else if (optional_params.Length == 2)
            {
                boardWidth = (int)optional_params[0];
                boardHeight = (int)optional_params[1];
                playerQueue = new PlayerQueue(player, game, elo, boardWidth, boardHeight);
            }
            else
            {
                playerQueue = new PlayerQueue(player, game, elo, null, null);
            }
            playerRepository.GetPlayerById(Guid.Empty);
            List<PlayerQueue> availablePlayers;
            PlayerQueueRepository playerQueueRepository = new PlayerQueueRepository(gameDbContext);
            availablePlayers = playerQueueRepository.GetPlayers();

            if (availablePlayers.Count == 0)
            {
                playerQueueRepository.AddPlayer(playerQueue);
                int minPort = 1024;
                int maxPort = 65535;

                // Random random = new Random();
                // int randomPort = random.Next(minPort, maxPort + 1);
                // while (!IsPortAvailable(randomPort))
                // {
                //    randomPort = random.Next(minPort, maxPort + 1);
                // }
                listener = new TcpListener(IPAddress.Any, 69);
                host = true;
                startPlayer = player.Id;
                listener.Start();
                socket = listener.AcceptSocket();
                opponentPlayer = ReceivePlayer();
            }
            else
            {
                Random random = new();
                int random_idx = random.Next(0, availablePlayers.Count);
                PlayerQueue opponent = availablePlayers[random_idx];
                opponentPlayer = opponent.Player;
                startPlayer = opponentPlayer.Id;
                playerQueueRepository.RemovePlayer(opponent);
                client = new TcpClient("localhost", 69);
                socket = client.Client;
                SendPlayer(player);
            }
            if (host)
            {
                switch (gameType)
                {
                    case "Obstruction":

                        gameService = new ObstructionService(Guid.Empty, player, opponentPlayer, boardWidth, boardHeight, new ObstructionRepository(gameDbContext));
                        break;
                    case "Connect4":
                        gameService = new Connect4Service(Guid.Empty, player, opponentPlayer, new Connect4RepositoryDB(gameDbContext));
                        break;
                }
                SendGame(gameService.GetGame());
                switch (gameType)
                {
                    case "Obstruction":
                        gameRepo = new ObstructionRepository(gameDbContext);
                        break;
                    case "Connect4":
                        gameRepo = new Connect4RepositoryDB(gameDbContext);
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
            else
            {
                switch (gameType)
                {
                    case "Obstruction":
                        gameRepo = new ObstructionRepository(gameDbContext);
                        break;
                    case "Connect4":
                        gameRepo = new Connect4RepositoryDB(gameDbContext);
                        break;
                    default:
                        throw new NotImplementedException();
                }
                IGame game1 = ReceiveGame();
                switch (gameType)
                {
                    case "Obstruction":
                        gameService = new ObstructionService(game1.GameState.Id, player, opponentPlayer, boardWidth, boardHeight, new ObstructionRepository(gameDbContext));
                        break;
                    case "Connect4":
                        gameService = new Connect4Service(game1.GameState.Id, player, opponentPlayer, new Connect4RepositoryDB(gameDbContext));
                        break;
                }
            }
        }
        private static bool IsPortAvailable(int port)
        {
            try
            {
                var listener = new TcpListener(IPAddress.Any, port);
                listener.Start();
                listener.Stop();
                return true;
            }
            catch (SocketException)
            {
                return false;
            }
        }

        public void Play(int nrParameters, object[] parameters)
        {
            if (firstTurn && !host)
            {
                throw new NotYourTurnException();
            }
            if (GetTurn() != player.Id)
            {
                throw new NotYourTurnException();
            }

            IGame newGame = gameService.Play(nrParameters, parameters);
            SendGame(newGame);
        }

        private void SendPlayer(Player player)
        {
            // MemoryStream stream = new MemoryStream();
            // #pragma warning disable SYSLIB0011 // Type or member is obsolete
            //            BinaryFormatter formatter = new();
            // #pragma warning restore SYSLIB0011 // Type or member is obsolete
            //            formatter.Serialize(stream, player);
            //            long length = stream.Length;
            //            socket.Send(BitConverter.GetBytes(length));
            //            byte[] buffer = stream.ToArray();
            //            socket.Send(buffer);
            string playerId = player.Id.ToString();
            byte[] idBuffer = Encoding.ASCII.GetBytes(playerId);
            socket.Send(idBuffer);
        }

        private void SendGame(IGame game)
        {
            string gameStateId = game.GameState.Id.ToString();
            byte[] idBuffer = Encoding.ASCII.GetBytes(gameStateId);
            try
            {
                socket.Send(idBuffer);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private IGame ReceiveGame()
        {
            byte[] buffer = new byte[1024];
            try
            {
                int bytesRead = socket.Receive(buffer);
                string gameStateId = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                IGame? game = gameRepo.GetGameById(Guid.Parse(gameStateId));
                if (game == null)
                {
                    game = gameRepo.GetGameFromDatabase(Guid.Parse(gameStateId));
                }

                return game;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        private Player ReceivePlayer()
        {
            byte[] buffer = new byte[1024];
            int bytesRead = socket.Receive(buffer);
            string playerId = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            return playerRepository.GetPlayerById(Guid.Parse(playerId));
        }

        public bool IsGameOver()
        {
            if (gameService.GetGame().GameState.Winner != null)
            {
                // statsService.UpdateStats(gameService.GetGame().GameState.Winner);
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
            gameService.SetGame(ReceiveGame());
        }

        public Guid? GetWinner()
        {
            return gameService.GetGame().GameState.Winner.Id;
        }

        public bool HasData()
        {
            return socket.Available > 0;
        }

        public Guid StartPlayer()
        {
            return startPlayer;
        }
    }
}

