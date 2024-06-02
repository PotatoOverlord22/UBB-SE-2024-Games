using GameWorldClassLibrary.Models;

namespace GameWorldClassLibrary.Repositories
{
    public interface ICommentRepository
    {
        Task AddCommentAsync(Comment comment);
        Task DeleteCommentAsync(Guid id);
        Task<Comment> GetCommentByIdAsync(Guid id);
        Task<List<Comment>> GetCommentsAsync();
        Task UpdateCommentAsync(Comment comment);
        Task<List<Comment>> GetUserCommentsAsync(Guid userId);
    }
}