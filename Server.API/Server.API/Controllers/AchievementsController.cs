using Microsoft.AspNetCore.Mvc;
using GameWorldClassLibrary.Models;
using GameWorldClassLibrary.Repositories;

namespace Server.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AchievementsController : ControllerBase
    {
        private readonly IAchievementRepository achievementRepository;

        public AchievementsController(IAchievementRepository achievementRepository)
        {
            this.achievementRepository = achievementRepository;
        }
        // Get all achievements
        // GET: api/achievements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Achievement>>> GetAchivements()
        {
            var achievements = await achievementRepository.GetAllAchievementsAsync();
            return achievements;
        }

        // Get Achievement by id
        // api/achievements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Achievement>> GetAchievementById(Guid id)
        {
            var achievement = await achievementRepository.GetAchievementByIdAsync(id);

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
                await achievementRepository.UpdateAchievementAsync(achievement);
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
                await achievementRepository.AddAchievementAsync(achievement);
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
                await achievementRepository.DeleteAchievementAsync(id);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}