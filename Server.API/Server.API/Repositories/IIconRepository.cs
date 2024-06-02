using GameWorldClassLibrary.Models;

namespace Server.API.Repositories
{
    public interface IIconRepository
    {
        Task<Icon> GetIconByIdAsync(Guid id);
        Task<List<Icon>> GetIconsAsync();
        Task AddIconAsync(Icon icon);
        Task DeleteIconAsync(Guid id);
        Task UpdateIconAsync(Guid id, Icon icon);
    }
}