using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.API.Models;
using Server.API.Utils;

namespace Server.API.Services
{
    public class UserService : IUserService
    {
        private readonly GamesContext gamesContext;
        public UserService(GamesContext context)
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
    }
}
