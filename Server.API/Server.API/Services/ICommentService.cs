using Microsoft.AspNetCore.Mvc;
using Server.API.Models;

namespace Server.API.Services
{
    public interface ICommentService
    {
        Task AddCommentAsync(Comment comment);
        Task DeleteCommentAsync(Guid id);
        Task<ActionResult<Achievement>> GetCommentByIdAsync(Guid id);
        Task<ActionResult<IEnumerable<Comment>>> GetCommentsAsync();
        Task UpdateCommentAsync(Guid id, Comment comment);
    }
}