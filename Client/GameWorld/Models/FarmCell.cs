namespace GameWorld.Models
{
    public class FarmCell
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public Item Item { get; set; }
        public DateTime? LastTimeEnhanced { get; set; } // Nullable.
        public DateTime? LastTimeInteracted { get; set; } // Nullable.
        public FarmCell(Guid id, User user, int row, int column, Item item, DateTime? lastTimeEnhanced, DateTime? lastTimeInteracted)
        {
            Id = id;
            User = user;
            Row = row;
            Column = column;
            Item = item;
            LastTimeEnhanced = lastTimeEnhanced;
            LastTimeInteracted = lastTimeInteracted;
        }
    }
}
