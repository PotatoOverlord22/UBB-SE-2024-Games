using Microsoft.EntityFrameworkCore;
using GameWorldClassLibrary.Models;
using Server.API.Utils;

namespace Server.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly GamesContext gamesContext;
        public UserRepository(GamesContext context)
        {
            gamesContext = context;
        }
        public async Task AddUser(User user)
        {
            gamesContext.Users.Add(user);
            await gamesContext.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(Guid id)
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

        public async Task<List<User>> GetUsersAsync()
        {
            return await gamesContext.Users.ToListAsync();
        }

        public async Task UpdateUserAsync(Guid id, User user)
        {
            if (gamesContext.Users.Find(id) == null)
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

        public async Task<List<User>> GetPokerLeaderboardAsync()
        {
            var leaderboard = await gamesContext.Users
                .OrderByDescending(user => user.UserChips)
                .ThenByDescending(user => user.UserLevel)
                .ThenBy(user => user.Username)
                .ToListAsync();

            return leaderboard;
        }
    }
}
