using Microsoft.AspNetCore.Mvc;
using GameWorldClassLibrary.Models;
using GameWorldClassLibrary.Repositories;

namespace Server.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemRepository itemService;

        public ItemsController(IItemRepository itemService)
        {
            this.itemService = itemService;
        }

        // Get all items
        // GET: api/items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems()
        {
            var items = await itemService.GetAllItemsAsync();
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
                await itemService.UpdateItemAsync(item);
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
                await itemService.CreateItemAsync(item);
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
                await itemService.DeleteItemAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}