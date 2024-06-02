using Microsoft.AspNetCore.Mvc;
using Server.API.Models;
using Server.API.Repositories;

namespace Server.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TitlesController : ControllerBase
    {
        private readonly ITitleRepository titleService;

        public TitlesController(ITitleRepository titleService)
        {
            this.titleService = titleService;
        }

        // Get all titles
        // GET: api/titles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Title>>> GetTitles()
        {
            var titles = await titleService.GetTitleAsync();
            return titles;
        }

        // Get Title by id
        // api/titles/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Title>> GetTitleById(Guid id)
        {
            var title = await titleService.GetTitleByIdAsync(id);

            if (title == null)
            {
                return NotFound();
            }

            return title;
        }

        // UPDATE a title
        // PUT: api/titles/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTitle(Guid id, Title title)
        {
            try
            {
                await titleService.UpdateTitleAsync(id, title);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            return NoContent();
        }

        // Add title
        // POST: api/titles
        [HttpPost]
        public async Task<IActionResult> AddTitle(Title title)
        {
            try
            {
                await titleService.AddTitleAsync(title);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return NoContent();
        }

        // DELETE: api/titles/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTitle(Guid id)
        {
            try
            {
                await titleService.DeleteTitleAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
