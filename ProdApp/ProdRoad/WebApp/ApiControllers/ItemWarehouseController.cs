#nullable enable
using BLL.App.Contracts;
using BLL.App.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemWarehouseController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public ItemWarehouseController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/ItemWarehouse
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemWarehouse>>> GetItemWarehouses()
        {
            var dataList = (await _bll.ItemWarehouses
                    .GetAllAsync())
                .ToList();
            return dataList;
        }

        // GET: api/ItemWarehouse/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemWarehouse>> GetItemWarehouse(Guid id)
        {
            var itemWarehouse = await _bll.ItemWarehouses.FirstOrDefaultAsync(id);
            
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

            var dbRow = await _bll.ItemWarehouses.FirstOrDefaultAsync(id);
            
            if (dbRow == null) {return NotFound();}

            _bll.ItemWarehouses.ModifyState(dbRow);

            try
            {
                await _bll.SaveChangesAsync();
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
            
            _bll.ItemWarehouses.Add(dbRow);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetItemWarehouse", new { id = itemWarehouse.Id }, itemWarehouse);
        }

        // DELETE: api/ItemWarehouse/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemWarehouse(Guid id)
        {
            var itemWarehouse = await _bll.ItemWarehouses.FirstOrDefaultAsync(id);
            if (itemWarehouse == null)
            {
                return NotFound();
            }

            _bll.ItemWarehouses.Remove(itemWarehouse);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemWarehouseExists(Guid id)
        {
            return _bll.ItemWarehouses.Exists(id);
        }
    }
}
