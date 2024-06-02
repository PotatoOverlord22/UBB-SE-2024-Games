using Microsoft.EntityFrameworkCore;
using Server.API.Models;
using Server.API.Repositories;
using Server.API.Utils;

public class CommentRepository : ICommentRepository
{
    private readonly GamesContext context;

    public CommentRepository(GamesContext context)
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

    public async Task UpdateCommentAsync(Guid id, Comment comment)
    {
        if (context.Comments.Find(id) == null)
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
}
