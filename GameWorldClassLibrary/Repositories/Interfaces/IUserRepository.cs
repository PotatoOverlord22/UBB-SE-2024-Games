using GameWorldClassLibrary.Models;

namespace GameWorldClassLibrary.Repositories
{
    public interface IUserRepository
    {
        Task AddUserAsync(User user);
        Task<User> GetUserByIdAsync(Guid userId);
        Task<List<User>> GetAllUsersAsync();
        Task UpdateUserAsync(User user);
        Task DeleteUserByIdAsync(Guid userId);
        Task UpdateUserLastLogin(Guid id, DateTime lastLogin);
        Task<User> GetUserByUsername(string username);
    }
}
