using Microsoft.AspNetCore.Mvc;
using Server.API.Models;

namespace Server.API.Services
{
    public interface IAchievementService
    {
        Task<ActionResult<IEnumerable<Achievement>>> GetAchievementsAsync();
    }
}