namespace GameWorldClassLibrary.Models
{
    public class Comment
    {
        public Guid Id { get; set; }
        public User Poster { get; set; }
        public string CommentMessage { get; set; }
        public DateTime CreationTime { get; set; }

        // Parameterless constructor for EF Core
        public Comment() { }

        public Comment(Guid id, User poster, string commentMessage, DateTime creationTime)
        {
            Id = id;
            Poster = poster;
            CommentMessage = commentMessage;
            CreationTime = creationTime;
        }
    }
}
