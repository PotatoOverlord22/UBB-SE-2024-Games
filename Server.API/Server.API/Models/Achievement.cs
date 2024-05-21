namespace Server.API.Models
{
    public class Achievement
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public int NumberOfCoinsRewarded { get; set; }

        public Achievement(Guid id, string description, int numberOfCoinsRewarded)
        {
            Id = id;
            Description = description;
            NumberOfCoinsRewarded = numberOfCoinsRewarded;
        }
    }
}