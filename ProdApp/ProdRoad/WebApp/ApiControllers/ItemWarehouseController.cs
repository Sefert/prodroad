#nullable enable
using DAL.App.Contracts;
using DAL.App.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemWarehouseController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public ItemWarehouseController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/ItemWarehouse
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemWarehouse>>> GetItemWarehouses()
        {
            var dataList = (await _uow.ItemWarehouses
                    .GetAllAsync())
                .ToList();
            return dataList;
        }

        // GET: api/ItemWarehouse/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemWarehouse>> GetItemWarehouse(Guid id)
        {
            var itemWarehouse = await _uow.ItemWarehouses.FirstOrDefaultAsync(id);
            
            if (itemWarehouse == null)
            {
                return NotFound();
            }

            return itemWarehouse;
        }

        // PUT: api/ItemWarehouse/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemWarehouse(Guid id, ItemWarehouse itemWarehouse)
        {
            if (id != itemWarehouse.Id)
            {
                return BadRequest();
            }

            var dbRow = await _uow.ItemWarehouses.FirstOrDefaultAsync(id);
            
            if (dbRow == null) {return NotFound();}

            _uow.ItemWarehouses.ModifyState(dbRow);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemWarehouseExists(id))
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

        // POST: api/ItemWarehouse
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ItemWarehouse>> PostItemWarehouse(ItemWarehouse itemWarehouse)
        {
            var dbRow = new ItemWarehouse()
            {
                ItemId = itemWarehouse.ItemId,
                WarehouseId = itemWarehouse.WarehouseId,
                Quantity = itemWarehouse.Quantity
            };
            
            _uow.ItemWarehouses.Add(dbRow);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetItemWarehouse", new { id = itemWarehouse.Id }, itemWarehouse);
        }

        // DELETE: api/ItemWarehouse/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemWarehouse(Guid id)
        {
            var itemWarehouse = await _uow.ItemWarehouses.FirstOrDefaultAsync(id);
            if (itemWarehouse == null)
            {
                return NotFound();
            }

            _uow.ItemWarehouses.Remove(itemWarehouse);
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemWarehouseExists(Guid id)
        {
            return _uow.ItemWarehouses.Exists(id);
        }
    }
}
