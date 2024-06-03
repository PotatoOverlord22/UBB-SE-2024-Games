namespace GameWorldClassLibrary.Models
{
    public class Games
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public List<GameStats> GameStats { get; set; }
        public List<PlayerQueue> Queues { get; set; }

        public Games(string name, string category)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.Category = category;
        }

        public Games(Guid id, string name, string category)
        {
            this.Id = id;
            this.Name = name;
            this.Category = category;
        }

        public Games()
        {
            this.Id = Guid.Empty;
            this.Name = string.Empty;
            this.Category = string.Empty;
        }

        public Games(string name)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.Category = string.Empty;
        }
    }
}