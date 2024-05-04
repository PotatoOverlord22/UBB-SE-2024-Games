namespace HarvestHaven.Entities
{
    public class Comment
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Message { get; set; }
        public DateTime CreatedTime { get; set; }

        public Comment(Guid id, Guid userId, string message, DateTime createdTime)
        {
            Id = id;
            UserId = userId;
            Message = message;
            CreatedTime = createdTime;
        }
    }
}
