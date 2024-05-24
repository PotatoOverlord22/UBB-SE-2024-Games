using Microsoft.AspNetCore.Mvc;
using Server.API.Models;
using Server.API.Services;

namespace Server.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TradesController : ControllerBase
    {
        private readonly ITradeService tradeService;

        public TradesController(ITradeService titleService)
        {
            this.tradeService = titleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trade>>> GetTrades()
        {
            var titles = await tradeService.GetTradesAsync();
            return titles;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Trade>> GetTradeById(Guid id)
        {
            var title = await tradeService.GetTradeByIdAsync(id);

            if (title == null)
            {
                return NotFound();
            }

            return title;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTrade(Guid id, Trade trade)
        {
            try
            {
                await tradeService.UpdateTradeAsync(id, trade);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> AddTrade(Trade trade)
        {
            try
            {
                await tradeService.AddTradeAsync(trade);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return NoContent();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteTrade(Guid id)
        {
            try
            {
                await tradeService.DeleteTradeAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
