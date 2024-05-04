namespace HarvestHaven.Entities
{
    public class FarmCell
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public Guid ItemId { get; set; }
        public DateTime? LastTimeEnhanced { get; set; } // Nullable.
        public DateTime? LastTimeInteracted { get; set; } // Nullable.

        public FarmCell(Guid id, Guid userId, int row, int column, Guid itemId, DateTime? lastTimeEnhanced, DateTime? lastTimeInteracted)
        {
            Id = id;
            UserId = userId;
            Row = row;
            Column = column;
            ItemId = itemId;
            LastTimeEnhanced = lastTimeEnhanced;
            LastTimeInteracted = lastTimeInteracted;
        }
    }
}
