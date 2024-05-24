using Server.API.Models;

namespace Server.API.Services
{
    public interface IIconService
    {
        Task<Icon> GetIconByIdAsync(Guid id);
        Task<List<Icon>> GetIconsAsync();
        Task AddIconAsync(Icon icon);
        Task DeleteIconAsync(Guid id);
        Task UpdateIconAsync(Guid id, Icon icon);
    }
}