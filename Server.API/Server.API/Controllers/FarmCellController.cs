using Microsoft.AspNetCore.Mvc;
using GameWorldClassLibrary.Models;
using GameWorldClassLibrary.Repositories;

namespace Server.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FarmCellController : ControllerBase
    {
        private readonly IFarmCellRepository farmCellService;

        public FarmCellController(IFarmCellRepository farmCellService)
        {
            this.farmCellService = farmCellService;
        }

        [HttpGet("{userID}")]
        public async Task<ActionResult<IEnumerable<FarmCell>>> GetFarmCellsAsync(Guid userID)
        {
            var farmCells = await farmCellService.GetUserFarmCellsAsync(userID);
            return farmCells;
        }

        [HttpGet("userId={userId}&row={row}&column={column}")]
        public async Task<ActionResult<FarmCell>> GetFarmCellByUserIdAndPositionAsync(Guid id, int row, int column)
        {
            var farmCell = await farmCellService.GetUserFarmCellByPositionAsync(id, row, column);

            if (farmCell == null)
            {
                return NotFound();
            }

            return farmCell;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFarmCell(FarmCell farmCell)
        {
            try
            {
                await farmCellService.UpdateFarmCellAsync(farmCell);
            }
            catch (KeyNotFoundException exception)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> AddinventoryResource(FarmCell farmCell)
        {
            try
            {
                await farmCellService.AddFarmCellAsync(farmCell);
            }
            catch (Exception exception)
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventoryResource(Guid farmCellId)
        {
            try
            {
                await farmCellService.DeleteFarmCellAsync(farmCellId);
            }
            catch (KeyNotFoundException exception)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
