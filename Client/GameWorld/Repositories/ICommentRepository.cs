using GameWorld.Entities;

namespace GameWorld.Repositories
{
    public interface ICommentRepository
    {
        Task CreateCommentAsync(Comment comment);
        Task<List<Comment>> GetUserCommentsAsync(Guid userId);
        Task UpdateCommentAsync(Comment comment);
        Task DeleteCommentAsync(Guid commentId);
    }
}
