using Microsoft.AspNetCore.Mvc;
using Server.API.Models;
using Server.API.Repositories;

namespace Server.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TablesController : ControllerBase
    {
        private readonly ITableRepository tableService;

        public TablesController(ITableRepository tableService)
        {
            this.tableService = tableService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Table>>> GetTables()
        {
            var tables = await tableService.GetTableAsync();
            return tables;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Table>> GetTableById(Guid id)
        {
            var table = await tableService.GetTableByIdAsync(id);

            if (table == null)
            {
                return NotFound();
            }

            return table;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTable(Guid id, Table table)
        {
            try
            {
                await tableService.UpdateTableAsync(id, table);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> AddTable(Table table)
        {
            try
            {
                await tableService.AddTableAsync(table);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTable(Guid id)
        {
            try
            {
                await tableService.DeleteTableAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}