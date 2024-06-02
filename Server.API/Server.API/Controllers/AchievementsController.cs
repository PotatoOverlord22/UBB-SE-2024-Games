using Microsoft.AspNetCore.Mvc;
using GameWorldClassLibrary.Models;
using GameWorldClassLibrary.Repositories.Interfaces;

namespace Server.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AchievementsController : ControllerBase
    {
        private readonly IAchievementRepository achievementService;

        public AchievementsController(IAchievementRepository achievementService)
        {
            this.achievementService = achievementService;
        }

        // Get all achievements
        // GET: api/achievements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Achievement>>> GetAchivements()
        {
            var achievements = await achievementService.GetAllAchievementsAsync();
            return achievements;
        }

        // Get Achievement by id
        // api/achievements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Achievement>> GetAchievementById(Guid id)
        {
            var achievement = await achievementService.GetAchievementByIdAsync(id);

            if (achievement == null)
            {
                return NotFound();
            }

            return achievement;
        }

        // UPDATE an achievement
        // PUT: api/achievements/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAchievement(Guid id, Achievement achievement)
        {
            try
            {
                await achievementService.UpdateAchievementAsync(achievement);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound();
            }
            return NoContent();
        }

        // Add achievement
        // POST: api/achievements
        [HttpPost]
        public async Task<IActionResult> AddAchievement(Achievement achievement)
        {
            try
            {
                await achievementService.AddAchievementAsync(achievement);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
            return NoContent();
        }

        // DELETE: api/achievements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAchievement(Guid id)
        {
            try
            {
                await achievementService.DeleteAchievementAsync(id);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}