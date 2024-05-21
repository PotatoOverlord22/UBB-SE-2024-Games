using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.API.Models;
using Server.API.Utils;

namespace Server.API.Services
{
    public class AchievementService : IAchievementService
    {
        private readonly GamesContext context;

        public AchievementService(GamesContext context)
        {
            this.context = context;
        }

        public async Task<ActionResult<IEnumerable<Achievement>>> GetAchievementsAsync()
        {
            return await context.Achievements.ToListAsync();
        }
    }
}
