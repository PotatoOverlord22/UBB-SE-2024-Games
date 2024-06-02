using Server.API.Models;

namespace Server.API.Repositories
{
    public interface IFontRepository
    {
        Task<Font> GetFontByIdAsync(Guid id);
        Task<List<Font>> GetFontsAsync();
        Task AddFontAsync(Font font);
        Task DeleteFontAsync(Guid id);
        Task UpdateFontAsync(Guid id, Font font);
    }
}