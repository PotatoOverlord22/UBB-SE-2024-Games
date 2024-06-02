using Microsoft.AspNetCore.Mvc;
using Server.API.Models;
using Server.API.Repositories;

namespace Server.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopItemsController : ControllerBase
    {
        private readonly IShopItemRepository shopItemsService;

        public ShopItemsController(IShopItemRepository shopItemsService)
        {
            this.shopItemsService = shopItemsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShopItem>>> GetShopItems()
        {
            var shopItem = await shopItemsService.GetShopItemAsync();
            return shopItem;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ShopItem>> GetShopItemById(Guid id)
        {
            var shopItem = await shopItemsService.GetShopItemByIdAsync(id);

            if (shopItem == null)
            {
                return NotFound();
            }

            return shopItem;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShopItem(Guid id, ShopItem shopItem)
        {
            try
            {
                await shopItemsService.UpdateShopItemAsync(id, shopItem);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> AddShopItem(ShopItem shopItem)
        {
            try
            {
                await shopItemsService.AddShopItemAsync(shopItem);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShopItem(Guid id)
        {
            try
            {
                await shopItemsService.DeleteShopItemAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}