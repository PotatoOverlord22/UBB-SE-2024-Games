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
        Task UpdateUserChipsAsync(Guid id, int chips);
        Task UpdateUserStreak(Guid id, int streak);
        Task UpdateUserLastLogin(Guid id, DateTime lastLogin);
        Task UpdateUserStack(Guid id, int stack);
        Task<List<User>> GetPokerLeaderboard();
    }
}
