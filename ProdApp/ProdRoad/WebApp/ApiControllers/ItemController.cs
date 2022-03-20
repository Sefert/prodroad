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
    public class ItemController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ItemController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Item
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDTO>>> GetItems()
        {
            var dataList = (await _context.Items
                    .ToListAsync())
                .Select(row => new ItemDTO()
                {
                    ItemId = row.ItemId,
                    AppUserId = row.AppUserId,
                    Name = row.Name,
                    Type = row.Type,
                    Unit = row.Unit,
                    Quantity = row.Quantity
                })
                .ToList();
            return dataList;
        }

        // GET: api/Item/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDTO>> GetItem(Guid id)
        {
            var dbRow = await _context.Items.FindAsync(id);
            
            if (dbRow == null)
            {
                return NotFound();
            }
            
            var item = new ItemDTO()
            {
                Id = dbRow.Id,
                AppUserId = dbRow.AppUserId,
                Name = dbRow.Name,
                Type = dbRow.Type,
                Unit = dbRow.Unit,
                Quantity = dbRow.Quantity
            };

            return item;
        }

        // PUT: api/Item/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(Guid id, ItemDTO item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            var dbRow = await _context.Items.FindAsync(id);
            
            if (dbRow == null) {return NotFound();}

            dbRow.Name!.SetTranslation(item.Name!);
            dbRow.Type!.SetTranslation(item.Type!);
            dbRow.Unit!.SetTranslation(item.Unit!);

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
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

        // POST: api/Item
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ItemDTO>> PostItem(ItemDTO item)
        {
            var dbRow = new Item()
            {
                AppUserId = item.AppUserId,
                Quantity = item.Quantity
            };
            
            dbRow.Name!.SetTranslation(item.Name!);
            dbRow.Type!.SetTranslation(item.Type!);
            dbRow.Unit!.SetTranslation(item.Unit!);
            
            _context.Items.Add(dbRow);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItem", new { id = item.Id }, item);
        }

        // DELETE: api/Item/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemExists(Guid id)
        {
            return _context.Items.Any(e => e.Id == id);
        }
    }
}
