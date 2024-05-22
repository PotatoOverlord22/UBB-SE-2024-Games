namespace GameWorld.Models
{
    public class Comment
    {
        public Guid Id { get; set; }
        public User Poster { get; set; }
        public string CommentMessage { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
