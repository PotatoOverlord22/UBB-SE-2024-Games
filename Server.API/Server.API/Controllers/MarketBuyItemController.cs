using Microsoft.AspNetCore.Mvc;
using GameWorldClassLibrary.Models;
using Server.API.Repositories;

namespace Server.API.Controllers
{
    public class MarketBuyItemController : ControllerBase
    {
        private readonly MarketBuyItemRepository marketBuyItemService;

        public MarketBuyItemController(MarketBuyItemRepository marketBuyItemService)
        {
            this.marketBuyItemService = marketBuyItemService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MarketBuyItem>>> GetMarketBuyItems()
        {
            var marketBuyItems = await marketBuyItemService.GetAllMarketBuyItemsAsync();
            return marketBuyItems;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MarketBuyItem>> GetMarketBuyItemsById(Guid marketBuyItemId)
        {
            var marketBuyItem = await marketBuyItemService.GetMarketBuyItemByItemIdAsync(marketBuyItemId);

            if (marketBuyItem == null)
            {
                return NotFound();
            }

            return marketBuyItem;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMarketBuyItem(Guid marketBuyItemId, MarketBuyItem marketBuyItem)
        {
            try
            {
                await marketBuyItemService.UpdateMarketBuyItemAsync(marketBuyItem);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> AddMarketBuyItem(MarketBuyItem marketBuyItem)
        {
            try
            {
                await marketBuyItemService.AddMarketBuyItemAsync(marketBuyItem);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
            return NoContent();
        }

        // DELETE: api/achievements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAchievement(Guid marketBuyItemId)
        {
            try
            {
                await marketBuyItemService.DeleteMarketBuyItemAsync(marketBuyItemId);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
