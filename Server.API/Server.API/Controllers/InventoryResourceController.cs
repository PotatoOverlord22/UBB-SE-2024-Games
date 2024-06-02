using Microsoft.AspNetCore.Mvc;
using GameWorldClassLibrary.Models;
using Server.API.Repositories;

namespace Server.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryResourceController : ControllerBase
    {
        private readonly IInventoryResourceRepository inventoryResourceService;

        public InventoryResourceController(IInventoryResourceRepository inventoryResourceService)
        {
            this.inventoryResourceService = inventoryResourceService;
        }

        [HttpGet("{userID}")]
        public async Task<ActionResult<IEnumerable<InventoryResource>>> GetUserResourcesAsync(Guid userID)
        {
            var userResource = await inventoryResourceService.GetUserResourcesAsync(userID);
            return userResource;
        }

        // Get Achievement by id
        // api/achievements/5
        [HttpGet("userId={userId}&resourceId={resourceId}")]
        public async Task<ActionResult<InventoryResource>> GetUserResourceByResourceIdAsync(Guid id, Guid resourceID)
        {
            var userResources = await inventoryResourceService.GetUserResourceByResourceIdAsync(id, resourceID);

            if (userResources == null)
            {
                return NotFound();
            }

            return userResources;
        }

        // UPDATE an achievement
        // PUT: api/achievements/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInventoryResource(InventoryResource inventoryResource)
        {
            try
            {
                await inventoryResourceService.UpdateUserResourceAsync(inventoryResource);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound();
            }
            return NoContent();
        }

        // Add achievement
        // POST: api/achievements
        [HttpPost]
        public async Task<IActionResult> AddinventoryResource(InventoryResource inventoryResource)
        {
            try
            {
                await inventoryResourceService.AddUserResourceAsync(inventoryResource);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
            return NoContent();
        }

        // DELETE: api/achievements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventoryResource(Guid id)
        {
            try
            {
                await inventoryResourceService.DeleteUserResourceAsync(id);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
