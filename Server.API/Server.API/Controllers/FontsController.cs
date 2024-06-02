using Microsoft.AspNetCore.Mvc;
using Server.API.Models;
using Server.API.Repositories;

namespace Server.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FontsController : ControllerBase
    {
        private readonly IFontRepository fontService;

        public FontsController(IFontRepository fontService)
        {
            this.fontService = fontService;
        }

        // Get all fonts
        // GET: api/fonts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Font>>> GetFonts()
        {
            var fonts = await fontService.GetFontsAsync();
            return fonts;
        }

        // Get font by id
        // api/fonts/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Font>> GetFontById(Guid id)
        {
            var font = await fontService.GetFontByIdAsync(id);

            if (font == null)
            {
                return NotFound();
            }

            return font;
        }

        // UPDATE an font
        // PUT: api/fonts/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFont(Guid id, Font font)
        {
            try
            {
                await fontService.UpdateFontAsync(id, font);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            return NoContent();
        }

        // Add font
        // POST: api/fonts
        [HttpPost]
        public async Task<IActionResult> AddFont(Font font)
        {
            try
            {
                await fontService.AddFontAsync(font);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return NoContent();
        }

        // DELETE: api/fonts/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFont(Guid id)
        {
            try
            {
                await fontService.DeleteFontAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}