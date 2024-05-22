using Microsoft.AspNetCore.Mvc;
using Server.API.Models;
using Server.API.Services;

namespace Server.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService itemService;

        public ItemsController(IItemService itemService)
        {
            this.itemService = itemService;
        }

        // Get all items
        // GET: api/items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems()
        {
            var items = await itemService.GetItemsAsync();
            return items;
        }

        // Get Item by id
        // api/items/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItemById(Guid id)
        {
            var item = await itemService.GetItemByIdAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // UPDATE an item
        // PUT: api/items/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(Guid id, Item item)
        {
            try
            {
                itemService.UpdateItem(id, item);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            return NoContent();
        }

        // Add item
        // POST: api/items
        [HttpPost]
        public async Task<IActionResult> AddItem(Item item)
        {
            try
            {
                itemService.AddItem(item);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return NoContent();
        }

        // DELETE: api/items/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            try
            {
                itemService.DeleteItem(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}