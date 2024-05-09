namespace HarvestHaven.Entities
{
    public class Comment(Guid id, Guid posterUserId, string commentMessage, DateTime commentCreationTime)
    {
        public Guid Id { get; set; } = id;
        public Guid PosterUserId { get; set; } = posterUserId;
        public string CommentMessage { get; set; } = commentMessage;
        public DateTime CreationTime { get; set; } = commentCreationTime;
    }
}
