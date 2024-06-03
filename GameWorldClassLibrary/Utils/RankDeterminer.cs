namespace GameWorldClassLibrary.Utils
{
    public class RankDeterminer
    {
        public static string DetermineRank(int elo)
        {
            if (elo < 500)
            {
                return "Bronze";
            }
            else if (elo < 1000)
            {
                return "Silver";
            }
            else if (elo < 1500)
            {
                return "Gold";
            }
            else
            {
                return "Diamonds";
            }
        }
    }
}
