#nullable enable
using DAL.App.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.App;
using WebApp.DTO;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public WarehouseController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Warehouse
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WarehouseDTO>>> GetWarehouses()
        {
            var dataList = (await _uow.Warehouses
                    .GetAllAsync())
                .Select(row => new WarehouseDTO()
                {
                    AppUserId = row.AppUserId,
                    Name = row.Name,
                    Address = row.Address
                })
                .ToList();
            return dataList;
        }

        // GET: api/Warehouse/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WarehouseDTO>> GetWarehouse(Guid id)
        {
            var dbRow = await _uow.Warehouses.FirstOrDefaultAsync(id);
            
            if (dbRow == null)
            {
                return NotFound();
            }
            
            var warehouse = new WarehouseDTO()
            {
                Id = dbRow.Id,
                AppUserId = dbRow.AppUserId,
                Name = dbRow.Name,
                Address = dbRow.Address
            };

            return warehouse;
        }

        // PUT: api/Warehouse/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWarehouse(Guid id, WarehouseDTO warehouse)
        {
            if (id != warehouse.Id)
            {
                return BadRequest();
            }

            var dbRow = await _uow.Warehouses.FirstOrDefaultAsync(id);
            
            if (dbRow == null) {return NotFound();}

            dbRow.Name!.SetTranslation(warehouse.Name!);
            dbRow.Address!.SetTranslation(warehouse.Address!);

            _uow.Warehouses.ModifyState(dbRow);

            try
            {
                await _uow.SaveChangesAsync();
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
        public async Task<ActionResult<Warehouse>> PostWarehouse(WarehouseDTO warehouse)
        {
            var dbRow = new Warehouse()
            {
                AppUserId = warehouse.AppUserId,
            };
            
            dbRow.Name!.SetTranslation(warehouse.Name!);
            dbRow.Address!.SetTranslation(warehouse.Address!);
            
            _uow.Warehouses.Add(dbRow);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetWarehouse", new { id = warehouse.Id }, warehouse);
        }

        // DELETE: api/Warehouse/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWarehouse(Guid id)
        {
            var warehouse = await _uow.Warehouses.FirstOrDefaultAsync(id);
            if (warehouse == null)
            {
                return NotFound();
            }

            _uow.Warehouses.Remove(warehouse);
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        private bool WarehouseExists(Guid id)
        {
            return _uow.Warehouses.Exists(id);
        }
    }
}
