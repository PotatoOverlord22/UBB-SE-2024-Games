using Microsoft.AspNetCore.Mvc;
using GameWorldClassLibrary.Models;
using Server.API.Repositories;

namespace Server.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceController
    {
        public class ResourcesController : ControllerBase
        {
            private readonly IResourceRepository resourceService;

            public ResourcesController(IResourceRepository resourceService)
            {
                this.resourceService = resourceService;
            }

            // Get all resources
            // GET: api/resources
            [HttpGet]
            public async Task<ActionResult<IEnumerable<Resource>>> GetResources()
            {
                var resources = await resourceService.GetAllResourcesAsync();
                return resources;
            }

            // Get Resource by id
            // api/resources/5
            [HttpGet("{id}")]
            public async Task<ActionResult<Resource>> GetResourceById(Guid resourceId)
            {
                var resource = await resourceService.GetResourceByIdAsync(resourceId);

                if (resource == null)
                {
                    return NotFound();
                }

                return resource;
            }

            [HttpGet("/type/{type}")]
            public async Task<ActionResult<Resource>> GetResourceByType(ResourceType resourceType)
            {
                var resource = await resourceService.GetResourceByTypeAsync(resourceType);

                if (resource == null)
                {
                    return NotFound();
                }

                return resource;
            }

            // UPDATE a resource
            // PUT: api/resources/5
            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateResource(Resource resource)
            {
                try
                {
                    await resourceService.UpdateResourceAsync(resource);
                }
                catch (KeyNotFoundException e)
                {
                    return NotFound();
                }
                return NoContent();
            }

            // Add resource
            // POST: api/resources
            [HttpPost]
            public async Task<IActionResult> AddResource(Resource resource)
            {
                try
                {
                    await resourceService.AddResourceAsync(resource);
                }
                catch (Exception e)
                {
                    return BadRequest();
                }
                return NoContent();
            }

            // DELETE: api/resources/5
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteResource(Guid id)
            {
                try
                {
                    await resourceService.DeleteResourceAsync(id);
                }
                catch (KeyNotFoundException e)
                {
                    return NotFound();
                }
                return NoContent();
            }
        }
    }
}
