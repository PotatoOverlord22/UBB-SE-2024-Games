using GameWorldClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameWorldClassLibrary.Repositories
{
    public interface ICommentRepository
    {
        Task AddCommentAsync(Comment comment);
        Task DeleteCommentAsync(Guid id);
        Task<Comment> GetCommentByIdAsync(Guid id);
        Task<List<Comment>> GetCommentsAsync();
        Task UpdateCommentAsync(Guid id, Comment comment);
    }
}