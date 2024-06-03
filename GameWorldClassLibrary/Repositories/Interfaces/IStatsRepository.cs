using GameWorldClassLibrary.Models;

namespace GameWorldClassLibrary.Repositories
{
    public interface IStatsRepository
    {
        public bool AddStats(GameStats gameStats);
        public bool UpdateStatsForPlayer(Player player, Games gameType, int newEloRating);
        public GameStats GetGameStatsForPlayer(Player player, Games gameType);
        public PlayerStats GetProfileStatsForPlayer(Player player);
    }
}