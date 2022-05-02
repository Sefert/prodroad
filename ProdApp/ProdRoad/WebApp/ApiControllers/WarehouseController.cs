#nullable enable
using BLL.App.Contracts;
using BLL.App.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public WarehouseController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Warehouse
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Warehouse>>> GetWarehouses()
        {
            var dataList = (await _bll.Warehouses
                    .GetAllAsync())
                .ToList();
            return dataList;
        }

        // GET: api/Warehouse/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Warehouse>> GetWarehouse(Guid id)
        {
            var warehouse = await _bll.Warehouses.FirstOrDefaultAsync(id);
            
            if (warehouse == null)
            {
                return NotFound();
            }

            return warehouse;
        }

        // PUT: api/Warehouse/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWarehouse(Guid id, Warehouse warehouse)
        {
            if (id != warehouse.Id)
            {
                return BadRequest();
            }

            var dbRow = await _bll.Warehouses.FirstOrDefaultAsync(id);
            
            if (dbRow == null) {return NotFound();}

            dbRow.Name!.SetTranslation(warehouse.Name!);
            dbRow.Address!.SetTranslation(warehouse.Address!);

            _bll.Warehouses.ModifyState(dbRow);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WarehouseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Warehouse
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Warehouse>> PostWarehouse(Warehouse warehouse)
        {
            var dbRow = new Warehouse()
            {
                AppUserId = warehouse.AppUserId,
            };
            
            dbRow.Name!.SetTranslation(warehouse.Name!);
            dbRow.Address!.SetTranslation(warehouse.Address!);
            
            _bll.Warehouses.Add(dbRow);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetWarehouse", new { id = warehouse.Id }, warehouse);
        }

        // DELETE: api/Warehouse/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWarehouse(Guid id)
        {
            var warehouse = await _bll.Warehouses.FirstOrDefaultAsync(id);
            if (warehouse == null)
            {
                return NotFound();
            }

            _bll.Warehouses.Remove(warehouse);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool WarehouseExists(Guid id)
        {
            return _bll.Warehouses.Exists(id);
        }
    }
}
