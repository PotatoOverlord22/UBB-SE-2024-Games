using Microsoft.EntityFrameworkCore;
using GameWorldClassLibrary.Models;
using GameWorldClassLibrary.Utils;

namespace GameWorldClassLibrary.Repositories
{
    public class CommentRepositoryDB : ICommentRepository
    {
        private readonly GamesContext context;

        public CommentRepositoryDB(GamesContext context)
        {
            this.context = context;
        }

        public async Task<List<Comment>> GetCommentsAsync()
        {
            return await context.Comments.ToListAsync();
        }

        public async Task<Comment> GetCommentByIdAsync(Guid id)
        {
            var comment = await context.Comments.FindAsync(id);

            if (comment == null)
            {
                throw new KeyNotFoundException("Comment not found");
            }

            return comment;
        }

        public async Task UpdateCommentAsync(Comment comment)
        {
            if (context.Comments.Find(comment.Id) == null)
            {
                throw new KeyNotFoundException("Comment not found");
            }

            context.Comments.Update(comment);
            await context.SaveChangesAsync();
        }

        public async Task AddCommentAsync(Comment comment)
        {
            context.Comments.Add(comment);
            await context.SaveChangesAsync();
        }

        public async Task DeleteCommentAsync(Guid id)
        {
            var comment = context.Comments.Find(id);
            if (comment == null)
            {
                throw new KeyNotFoundException("Comment not found");
            }
            context.Comments.Remove(comment);
            await context.SaveChangesAsync();
        }

        public async Task<List<Comment>> GetUserCommentsAsync(Guid userId)
        {
            return await context.Comments
                .Where(comment => comment.Poster.Id == userId)
                .ToListAsync();
        }
    }
}
