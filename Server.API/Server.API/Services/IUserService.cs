using Microsoft.AspNetCore.Mvc;
using Server.API.Models;

namespace Server.API.Services
{
    public interface IUserService
    {
        Task AddUser(User user);
        Task DeleteUserAsync(Guid id);
        Task<User> GetUserByIdAsync(Guid id);
        Task<List<User>> GetUsersAsync();
        Task UpdateUserAsync(Guid id, User user);
        Task UpdateUserChipsAsync(Guid id, int chips);
    }
}