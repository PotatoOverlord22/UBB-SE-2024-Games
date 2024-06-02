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

        public async Task UpdateUserChipsAsync(Guid id, int chips)
        {
            // Find the user by ID
            var user = await gamesContext.Users.FindAsync(id);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            // Update the user's chips
            user.UserChips = chips;

            // Save changes to the database
            await gamesContext.SaveChangesAsync();
        }

        public async Task<List<User>> GetPokerLeaderboard()
        {
            var leaderboard = await gamesContext.Users
                .OrderByDescending(user => user.UserChips)
                .ThenByDescending(user => user.UserLevel)
                .ThenBy(user => user.Username)
                .ToListAsync();

            return leaderboard;
        }

        public async Task UpdateUserStreak(Guid id, int streak)
        {
            var user = await gamesContext.Users.FindAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }
            user.UserStreak = streak;
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

        public async Task UpdateUserStack(Guid id, int stack)
        {
            var user = await gamesContext.Users.FindAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }
            user.UserStack = stack;
            await gamesContext.SaveChangesAsync();
        }
    }
}
