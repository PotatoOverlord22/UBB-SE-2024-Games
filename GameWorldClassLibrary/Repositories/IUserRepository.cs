using GameWorldClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameWorldClassLibrary.Repositories
{
    public interface IUserRepository
    {
        Task AddUser(User user);
        Task DeleteUserAsync(Guid id);
        Task<User> GetUserByIdAsync(Guid id);
        Task<List<User>> GetUsersAsync();
        Task UpdateUserAsync(Guid id, User user);
        Task UpdateUserChipsAsync(Guid id, int chips);
        Task<List<User>> GetPokerLeaderboardAsync();
    }
}