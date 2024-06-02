using GameWorldClassLibrary.Models;

namespace GameWorldClassLibrary.Repositories.Interfaces
{
    public interface ICommentRepository
    {
        Task CreateCommentAsync(Comment comment);
        Task<List<Comment>> GetUserCommentsAsync(Guid userId);
        Task UpdateCommentAsync(Comment comment);
        Task DeleteCommentAsync(Guid commentId);
    }
}
