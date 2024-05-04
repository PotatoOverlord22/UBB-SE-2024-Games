namespace HarvestHaven.Entities
{
    public class Comment(Guid id, Guid userId, string message, DateTime createdTime)
    {
        public Guid Id { get; set; } = id;
        public Guid UserId { get; set; } = userId;
        public string Message { get; set; } = message;
        public DateTime CreatedTime { get; set; } = createdTime;
    }
}
