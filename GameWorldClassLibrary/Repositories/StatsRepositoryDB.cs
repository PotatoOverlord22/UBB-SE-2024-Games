using GameWorldClassLibrary.Models;
using GameWorldClassLibrary.Utils;

namespace GameWorldClassLibrary.Repositories
{
    public class StatsRepositoryDB : IStatsRepository
    {
        private readonly GamesContext context;

        public StatsRepositoryDB(GamesContext context)
        {
            this.context = context;
        }

        public bool AddStats(GameStats gameStats)
        {
            try
            {
                context.GameStats.Add(gameStats);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateStatsForPlayer(Player player, Games gameType, int newEloRating)
        {
            try
            {
                var gameStats = context.GameStats.FirstOrDefault(gs => gs.Player == player && gs.Game == gameType);
                if (gameStats != null)
                {
                    gameStats.EloRating = newEloRating;
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public GameStats GetGameStatsForPlayer(Player player, Games gameType)
        {
            var gameStats = context.GameStats.FirstOrDefault(gs => gs.Player == player && gs.Game.Name == gameType.Name);
            return gameStats ?? new GameStats(player, gameType);
        }


        public PlayerStats GetProfileStatsForPlayer(Player player)
        {
            var playerStatsQuery = (from gs in context.GameStats
                                    where gs.Player == player
                                    group gs by gs.Player into g
                                    select new
                                    {
                                        Trophies = g.Sum(gs => gs.TotalWins),
                                        AverageElo = g.Average(gs => gs.EloRating),
                                        MostPlayedGame = g.OrderByDescending(gs => gs.Game).FirstOrDefault().Game
                                    }).FirstOrDefault();

            if (playerStatsQuery != null)
            {
                var rank = RankDeterminer.DetermineRank((int)playerStatsQuery.AverageElo);
                return new PlayerStats(player, playerStatsQuery.Trophies, (int)playerStatsQuery.AverageElo, rank, playerStatsQuery.MostPlayedGame);
            }
            else
            {
                return new PlayerStats(player);
            }
        }
    }
}
