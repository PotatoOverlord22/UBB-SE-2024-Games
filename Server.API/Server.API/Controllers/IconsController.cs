using Microsoft.AspNetCore.Mvc;
using Server.API.Models;
using Server.API.Repositories;

namespace Server.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IconsController : ControllerBase
    {
        private readonly IIconRepository iconService;

        public IconsController(IIconRepository iconService)
        {
            this.iconService = iconService;
        }

        // Get all icons
        // GET: api/icons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Icon>>> GetFonts()
        {
            var icons = await iconService.GetIconsAsync();
            return icons;
        }

        // Get icon by id
        // api/icons/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Icon>> GetIconById(Guid id)
        {
            var icon = await iconService.GetIconByIdAsync(id);

            if (icon == null)
            {
                return NotFound();
            }

            return icon;
        }

        // UPDATE an icon
        // PUT: api/icons/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIcon(Guid id, Icon icon)
        {
            try
            {
                await iconService.UpdateIconAsync(id, icon);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            return NoContent();
        }

        // Add icon
        // POST: api/icons
        [HttpPost]
        public async Task<IActionResult> AddIcon(Icon font)
        {
            try
            {
                await iconService.AddIconAsync(font);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return NoContent();
        }

        // DELETE: api/icons/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIcon(Guid id)
        {
            try
            {
                await iconService.DeleteIconAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}