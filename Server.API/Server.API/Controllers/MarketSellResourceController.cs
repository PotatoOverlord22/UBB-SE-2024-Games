using Microsoft.AspNetCore.Mvc;
using GameWorldClassLibrary.Models;
using GameWorldClassLibrary.Repositories;

namespace Server.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarketSellResourceController : ControllerBase
    {
        private readonly IMarketSellResourceRepository marketSellResourceService;

        public MarketSellResourceController(IMarketSellResourceRepository marketSellResourceService)
        {
            this.marketSellResourceService = marketSellResourceService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MarketSellResource>>> GetAllMarketSellResourcesAsync()
        {
            var marketSellResources = await marketSellResourceService.GetAllSellResourcesAsync();
            return marketSellResources;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MarketSellResource>> GetMarketSellResourceByResourceIdAsync(Guid marketSellResourceId)
        {
            var marketSellResource = await marketSellResourceService.GetMarketSellResourceByResourceIdAsync(marketSellResourceId);

            if (marketSellResource == null)
            {
                return NotFound();
            }

            return marketSellResource;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMarketSellResource(Guid marketSellResourceId, MarketSellResource marketSellResource)
        {
            try
            {
                await marketSellResourceService.UpdateMarketSellResourceAsync(marketSellResource);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> AddMarketSellResource(MarketSellResource marketSellResource)
        {
            try
            {
                await marketSellResourceService.AddMarketSellResourceAsync(marketSellResource);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMarketSellResource(Guid marketSellResourceId)
        {
            try
            {
                await marketSellResourceService.DeleteMarketSellResourceAsync(marketSellResourceId);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
