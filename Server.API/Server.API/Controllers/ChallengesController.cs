using Microsoft.AspNetCore.Mvc;
using Server.API.Models;
using Server.API.Services;

namespace Server.API.Controllers
{
    public class ChallengeController : ControllerBase
    {
        private readonly IChallengeService challengeService;

        public ChallengeController(IChallengeService challengeService)
        {
            this.challengeService = challengeService;
        }

        // Get all challenges
        // GET: api/challenges
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Challenge>>> GetChallenges()
        {
            var challenge = await challengeService.GetChallengeAsync();
            return challenge;
        }

        // Get Challenges by id
        // api/challenges/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Challenge>> GetChallengeById(Guid id)
        {
            var challenge = await challengeService.GetChallengeByIdAsync(id);

            if (challenge == null)
            {
                return NotFound();
            }

            return challenge;
        }

        // UPDATE an challenge
        // PUT: api/challenge/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateChallenge(Guid id, Challenge challenge)
        {
            try
            {
                await challengeService.UpdateChallengeAsync(id, challenge);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound();
            }
            return NoContent();
        }

        // Add challenges
        // POST: api/challenges
        [HttpPost]
        public async Task<IActionResult> AddChallenge(Challenge challenge)
        {
            try
            {
                await challengeService.AddChallengeAsync(challenge);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
            return NoContent();
        }

        // DELETE: api/challenges/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChallenge(Guid id)
        {
            try
            {
                await challengeService.DeleteChallengeAsync(id);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
