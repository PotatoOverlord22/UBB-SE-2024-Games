using HarvestHaven.Entities;

namespace HarvestHaven.Repositories
{
    public interface IUserRepository
    {
        Task AddUserAsync(User user);
        Task<User> GetUserByIdAsync(Guid userId);
        Task<List<User>> GetAllUsersAsync();
        Task UpdateUserAsync(User user);
        Task DeleteUserByIdAsync(Guid userId);
    }
}
