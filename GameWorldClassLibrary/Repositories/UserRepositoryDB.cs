using Microsoft.EntityFrameworkCore;
using GameWorldClassLibrary.Models;
using GameWorldClassLibrary.Utils;

namespace GameWorldClassLibrary.Repositories
{
    public class UserRepositoryDB : IUserRepository
    {
        private readonly GamesContext gamesContext;
        public UserRepositoryDB(GamesContext context)
        {
            gamesContext = context;
        }
        public async Task AddUserAsync(User user)
        {
            gamesContext.Users.Add(user);
            await gamesContext.SaveChangesAsync();
        }

        public async Task DeleteUserByIdAsync(Guid id)
        {
            var user = gamesContext.Users.Find(id);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }
            gamesContext.Users.Remove(user);
            await gamesContext.SaveChangesAsync();
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            var user = await gamesContext.Users.FindAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }
            return user;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await gamesContext.Users.ToListAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            if (gamesContext.Users.Find(user.Id) == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            gamesContext.Users.Update(user);
            await gamesContext.SaveChangesAsync();
        }

        public async Task UpdateUserLastLogin(Guid id, DateTime lastLogin)
        {
            var user = await gamesContext.Users.FindAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }
            user.UserLastLogin = lastLogin;
            await gamesContext.SaveChangesAsync();
        }

        public async Task<User> GetUserByUsername(string username)
        {
            var user = await gamesContext.Users.FirstOrDefaultAsync(user => user.Username == username);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }
            return user;
        }
    }
}
