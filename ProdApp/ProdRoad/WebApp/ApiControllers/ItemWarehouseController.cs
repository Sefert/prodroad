#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;
using WebApp.DTO;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemWarehouseController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ItemWarehouseController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ItemWarehouse
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemWarehouseDTO>>> GetItemWarehouses()
        {
            var dataList = (await _context.ItemWarehouses
                    .ToListAsync())
                .Select(row => new ItemWarehouseDTO()
                {
                    ItemId = row.ItemId,
                    WarehouseId = row.WarehouseId,
                    Quantity = row.Quantity
                })
                .ToList();
            return dataList;
        }

        // GET: api/ItemWarehouse/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemWarehouseDTO>> GetItemWarehouse(Guid id)
        {
            var dbRow = await _context.ItemWarehouses.FindAsync(id);
            
            if (dbRow == null)
            {
                return NotFound();
            }
            
            var itemWarehouse = new ItemWarehouseDTO()
            {
                Id = dbRow.Id,
                ItemId = dbRow.ItemId,
                WarehouseId = dbRow.WarehouseId,
                Quantity = dbRow.Quantity
            };

            return itemWarehouse;
        }

        // PUT: api/ItemWarehouse/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemWarehouse(Guid id, ItemWarehouseDTO itemWarehouse)
        {
            if (id != itemWarehouse.Id)
            {
                return BadRequest();
            }

            var dbRow = await _context.ItemWarehouses.FindAsync(id);
            
            if (dbRow == null) {return NotFound();}

            _context.Entry(itemWarehouse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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
        public async Task<ActionResult<ItemWarehouseDTO>> PostItemWarehouse(ItemWarehouseDTO itemWarehouse)
        {
            var dbRow = new ItemWarehouse()
            {
                ItemId = itemWarehouse.ItemId,
                WarehouseId = itemWarehouse.WarehouseId,
                Quantity = itemWarehouse.Quantity
            };
            
            _context.ItemWarehouses.Add(dbRow);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItemWarehouse", new { id = itemWarehouse.Id }, itemWarehouse);
        }

        // DELETE: api/ItemWarehouse/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemWarehouse(Guid id)
        {
            var itemWarehouse = await _context.ItemWarehouses.FindAsync(id);
            if (itemWarehouse == null)
            {
                return NotFound();
            }

            _context.ItemWarehouses.Remove(itemWarehouse);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemWarehouseExists(Guid id)
        {
            return _context.ItemWarehouses.Any(e => e.Id == id);
        }
    }
}
