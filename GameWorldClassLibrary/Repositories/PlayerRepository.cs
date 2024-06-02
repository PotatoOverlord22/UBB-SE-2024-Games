using GameWorldClassLibrary.Models;
using GameWorldClassLibrary.Utils;

namespace GameWorldClassLibrary.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly GamesContext context;
        public PlayerRepository(GamesContext gamesDbContext)
        {
            context = gamesDbContext;
            Players = GetAllPlayers();
            DrawPlayer = GetPlayerById(Guid.Empty);
            AIPlayer = GetPlayerById(Guid.Parse("00000000-0000-0000-0000-000000000001"));
        }

        public PlayerRepository()
        {
        }

        public static Dictionary<Guid, Player> Players { get; set; }
        public static Player DrawPlayer { get; set; }
        public static Player AIPlayer { get; set; }

        public Player? GetPlayerById(Guid id)
        {
            return Players.TryGetValue(id, out Player? value) ? value : null;
        }

        public Dictionary<Guid, Player> GetAllPlayers()
        {
            //add from GamesDbContext to players
            Dictionary<Guid, Player> players = new Dictionary<Guid, Player>();
            foreach (var player in context.Players)
            {
                players.Add(player.Id, player);
            }
            return players;
        }

        public bool AddPlayer(Player player)
        {
            context.Players.Add(player);
            return context.SaveChanges() == 1;
        }

        public bool RemoveAddressById(Guid id)
        {
            Player player = GetPlayerById(id);
            if (player == null)
            {
                return false;
            }
            context.Players.Remove(player);
            return context.SaveChanges() == 1;
        }

        public bool UpdatePlayer(Player player)
        {
            Player? playerToUpdate = GetPlayerById(player.Id);
            if (playerToUpdate == null)
            {
                return false;
            }
            playerToUpdate = player;
            return context.SaveChanges() == 1;
        }

        public bool RemovePlayerWhereNameEqualsTestOrUpdated()
        {
            Player? player = Players.Values.FirstOrDefault(x => x.Name == "Test" || x.Name == "Updated");
            if (player == null)
            {
                return false;
            }
            Players.Remove(player.Id);
            return true;
        }
    }
}
