namespace GameWorld.Models
{
    public class Comment
    {
        public Guid Id { get; set; }
        public User Poster { get; set; }
        public string CommentMessage { get; set; }
        public DateTime CreationTime { get; set; }

        public Comment(Guid id, User poster, string commentMessage, DateTime creationTime)
        {
            Id = id;
            Poster = poster;
            CommentMessage = commentMessage;
            CreationTime = creationTime;
        }
    }
}
