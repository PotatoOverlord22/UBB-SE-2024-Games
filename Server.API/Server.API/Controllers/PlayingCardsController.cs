using Microsoft.AspNetCore.Mvc;
using GameWorldClassLibrary.Models;
using Server.API.Repositories;

namespace Server.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayingCardsController : ControllerBase
    {
        private readonly IPlayingCardRepository playingCardService;

        public PlayingCardsController(IPlayingCardRepository playingCardsService)
        {
            this.playingCardService = playingCardsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayingCard>>> GetPlayingCards()
        {
            var playingCards = await playingCardService.GetPlayingCardAsync();
            return playingCards;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PlayingCard>> GetPlayingCardById(Guid id)
        {
            var playingCard = await playingCardService.GetPlayingCardByIdAsync(id);

            if (playingCard == null)
            {
                return NotFound();
            }

            return playingCard;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlayingCard(Guid id, PlayingCard playingCard)
        {
            try
            {
                await playingCardService.UpdatePlayingCardAsync(id, playingCard);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> AddPlayingCard(PlayingCard playingCard)
        {
            try
            {
                await playingCardService.AddPlayingCardAsync(playingCard);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayingCard(Guid id)
        {
            try
            {
                await playingCardService.DeletePlayingCardAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}