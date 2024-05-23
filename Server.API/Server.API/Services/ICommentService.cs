using Microsoft.AspNetCore.Mvc;
using Server.API.Models;

namespace Server.API.Services
{
    public interface ICommentService
    {
        Task AddCommentAsync(Comment comment);
        Task DeleteCommentAsync(Guid id);
        Task<Comment> GetCommentByIdAsync(Guid id);
        Task<List<Comment>> GetCommentsAsync();
        Task UpdateCommentAsync(Guid id, Comment comment);
    }
}