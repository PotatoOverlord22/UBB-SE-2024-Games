namespace HarvestHaven.Entities
{
    public class Achievement
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public int RewardCoins { get; set; }

        public Achievement(Guid id, string description, int rewardCoins)
        {
            Id = id;
            Description = description;
            RewardCoins = rewardCoins;
        }
    }
}
